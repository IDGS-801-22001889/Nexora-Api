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
public class CotizacionController : ControllerBase
{
    private readonly AppDbContext _context;

    // --- Costos fijos del servicio, ajustables aquí ---
    private const decimal CostoLicenciaAnual = 3600m; // app + panel, por flotilla
    private const decimal CostoInstalacionPorUnidad = 160m;
    private const decimal CostoCapacitacion = 800m; // costo fijo, una sola vez
    private const decimal MargenComercial = 1.30m;   // 30% de margen sobre el costo de fabricación
    private const decimal PorcentajeIva = 0.16m;

    public CotizacionController(AppDbContext context)
    {
        _context = context;
    }

    // POST api/cotizacion -> pública, cualquier visitante puede cotizar
    [HttpPost]
    public async Task<IActionResult> Create(CotizacionDto dto)
    {
        if (dto.NumeroUnidades <= 0)
            return BadRequest("El número de unidades debe ser mayor a 0.");

        var producto = await _context.Productos.FirstOrDefaultAsync();
        if (producto is null) return BadRequest("No hay producto configurado.");

        // --- Costo real de fabricación, tomado de la Receta ---
        var receta = await _context.Recetas.Include(r => r.MateriaPrima).ToListAsync();
        var costoMateriales = receta.Sum(r => r.CantidadRequerida * (r.MateriaPrima?.CostoUnitario ?? 0));
        var costoEnsamblaje = costoMateriales * (producto.PorcentajeEnsamblaje / 100);
        var costoFabricacion = costoMateriales + costoEnsamblaje;

        // --- Precio de venta por unidad (costo de fabricación + margen comercial) ---
        var precioUnitarioVenta = costoFabricacion * MargenComercial;

        // --- Armado de la cotización ---
        var costoGorras = precioUnitarioVenta * dto.NumeroUnidades;
        var costoInstalacion = dto.InstalacionIncluida ? CostoInstalacionPorUnidad * dto.NumeroUnidades : 0;
        var costoCapacitacion = dto.Capacitacion ? CostoCapacitacion : 0;

        var subtotal = costoGorras + CostoLicenciaAnual + costoInstalacion + costoCapacitacion;
        var iva = subtotal * PorcentajeIva;
        var total = subtotal + iva;

        int? idUsuario = null;
        if (User.Identity?.IsAuthenticated == true)
        {
            idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        var cotizacion = new Cotizacion
        {
            IdUsuario = idUsuario,
            NombreEmpresa = dto.NombreEmpresa,
            NombreContacto = dto.NombreContacto,
            Email = dto.Email,
            Telefono = dto.Telefono,
            NumeroUnidades = dto.NumeroUnidades,
            TipoTransporte = dto.TipoTransporte,
            CiudadRegion = dto.CiudadRegion,
            InstalacionIncluida = dto.InstalacionIncluida,
            Capacitacion = dto.Capacitacion,
            CostoUnitario = Math.Round(precioUnitarioVenta, 2),
            Subtotal = Math.Round(subtotal, 2),
            Iva = Math.Round(iva, 2),
            Total = Math.Round(total, 2)
        };

        _context.Cotizaciones.Add(cotizacion);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            cotizacion.IdCotizacion,
            desglose = new
            {
                costoUnitario = cotizacion.CostoUnitario,
                costoGorras = Math.Round(costoGorras, 2),
                licenciaAnual = CostoLicenciaAnual,
                instalacion = costoInstalacion,
                capacitacion = costoCapacitacion,
                subtotal = cotizacion.Subtotal,
                iva = cotizacion.Iva,
                total = cotizacion.Total
            }
        });
    }

    // GET api/cotizacion -> solo administrador, lista todas las cotizaciones recibidas
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetAll()
    {
        var cotizaciones = await _context.Cotizaciones
            .OrderByDescending(c => c.Fecha)
            .ToListAsync();

        return Ok(cotizaciones);
    }

    // GET api/cotizacion/mis-cotizaciones -> solo del cliente logueado, y que aún no se hayan usado
    [HttpGet("mis-cotizaciones")]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> GetMisCotizaciones()
    {
        var idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var cotizaciones = await _context.Cotizaciones
            .Where(c => c.IdUsuario == idUsuario && c.Estado == "Nueva")
            .OrderByDescending(c => c.Fecha)
            .ToListAsync();

        return Ok(cotizaciones);
    }
}