using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.DTOs;
using Nexora.Api.Models;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class CompraController : ControllerBase
{
    private readonly AppDbContext _context;

    public CompraController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var compras = await _context.Compras
            .Include(c => c.Proveedor)
            .Include(c => c.Detalles)
                .ThenInclude(d => d.MateriaPrima)
            .OrderByDescending(c => c.Fecha)
            .ToListAsync();

        return Ok(compras);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompraDto dto)
    {
        if (dto.Detalles is null || dto.Detalles.Count == 0)
            return BadRequest("La compra debe tener al menos un detalle.");

        var proveedor = await _context.Proveedores.FindAsync(dto.IdProveedor);
        if (proveedor is null) return BadRequest("Proveedor no encontrado.");

        var compra = new Compra
        {
            IdProveedor = dto.IdProveedor,
            Fecha = DateTime.UtcNow
        };

        decimal totalCompra = 0;

        foreach (var detalleDto in dto.Detalles)
        {
            var materia = await _context.MateriasPrimas.FindAsync(detalleDto.IdMateriaPrima);
            if (materia is null)
                return BadRequest($"Materia prima con id {detalleDto.IdMateriaPrima} no encontrada.");

            // --- Cálculo de Promedio Ponderado ---
            var stockAnterior = materia.Stock;
            var costoAnterior = materia.CostoUnitario;
            var cantidadNueva = detalleDto.Cantidad;
            var costoNuevo = detalleDto.CostoUnitario;

            var stockTotal = stockAnterior + cantidadNueva;

            materia.CostoUnitario = stockTotal == 0
                ? costoNuevo
                : ((stockAnterior * costoAnterior) + (cantidadNueva * costoNuevo)) / stockTotal;

            materia.Stock = stockTotal;
            // --- Fin del cálculo ---

            var detalle = new DetalleCompra
            {
                IdMateriaPrima = detalleDto.IdMateriaPrima,
                Cantidad = cantidadNueva,
                CostoUnitario = costoNuevo
            };

            compra.Detalles.Add(detalle);
            totalCompra += cantidadNueva * costoNuevo;
        }

        compra.Total = totalCompra;

        _context.Compras.Add(compra);
        await _context.SaveChangesAsync();

        return Ok(compra);
    }
}