using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.DTOs;
using Nexora.Api.Models;
using System.Security.Claims;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PedidoController : ControllerBase
{
    private readonly AppDbContext _context;

    public PedidoController(AppDbContext context)
    {
        _context = context;
    }

    private int IdUsuarioActual =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // POST api/pedido -> convierte una cotización propia en un pedido
    [HttpPost]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> Create(PedidoDto dto)
    {
        var cotizacion = await _context.Cotizaciones.FindAsync(dto.IdCotizacion);
        if (cotizacion is null) return NotFound("Cotización no encontrada.");

        if (cotizacion.IdUsuario != IdUsuarioActual)
            return Forbid();

        if (cotizacion.Estado != "Nueva")
            return BadRequest("Esta cotización ya fue utilizada para una compra.");

        var pedido = new Pedido
        {
            IdUsuario = IdUsuarioActual,
            IdCotizacion = cotizacion.IdCotizacion,
            Cantidad = cotizacion.NumeroUnidades,
            MetodoPago = dto.MetodoPago,
            Total = cotizacion.Total, // mismo precio ya calculado en la cotización
            Estado = "En espera"
        };

        cotizacion.Estado = "Convertida a pedido";

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        return Ok(pedido);
    }

    // GET api/pedido/mis-compras
    [HttpGet("mis-compras")]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> GetMisCompras()
    {
        var pedidos = await _context.Pedidos
            .Where(p => p.IdUsuario == IdUsuarioActual)
            .OrderByDescending(p => p.Fecha)
            .ToListAsync();

        return Ok(pedidos);
    }

    // GET api/pedido -> solo administrador
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetAll()
    {
        var pedidos = await _context.Pedidos
            .Include(p => p.Usuario)
            .OrderByDescending(p => p.Fecha)
            .ToListAsync();

        return Ok(pedidos.Select(p => new
        {
            p.IdPedido,
            p.Cantidad,
            p.MetodoPago,
            p.Total,
            p.Estado,
            p.MensajeAdmin,
            p.Fecha,
            nombreCliente = p.Usuario!.Nombre,
            emailCliente = p.Usuario.Email
        }));
    }

    // PUT api/pedido/5/gestionar -> solo administrador
    [HttpPut("{id}/gestionar")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Gestionar(int id, GestionarPedidoDto dto)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido is null) return NotFound();

        if (pedido.Estado != "En espera")
            return BadRequest("Este pedido ya fue gestionado.");

        if (dto.Estado == "Aceptado")
        {
            var producto = await _context.Productos.FirstOrDefaultAsync();
            if (producto is null) return BadRequest("No hay producto configurado.");

            if (producto.Stock < pedido.Cantidad)
                return BadRequest($"No hay stock suficiente. Disponible: {producto.Stock}, solicitado: {pedido.Cantidad}.");

            producto.Stock -= pedido.Cantidad;
            pedido.Estado = "Aceptado";
            pedido.MensajeAdmin = dto.MensajeAdmin;
        }
        else if (dto.Estado == "Rechazado por inventario")
        {
            pedido.Estado = "Rechazado por inventario";
            pedido.MensajeAdmin = dto.MensajeAdmin ?? "Sin stock suficiente. Espera reabastecimiento.";
        }
        else
        {
            return BadRequest("Estado no válido.");
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }
}