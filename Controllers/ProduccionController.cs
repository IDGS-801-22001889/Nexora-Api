using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.DTOs;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrador")]
public class ProduccionController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProduccionController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/produccion/disponible -> cuántas unidades se podrían fabricar HOY con el stock actual
    [HttpGet("disponible")]
    public async Task<IActionResult> GetDisponible()
    {
        var receta = await _context.Recetas.Include(r => r.MateriaPrima).ToListAsync();

        if (receta.Count == 0)
            return Ok(new { unidadesPosibles = 0, mensaje = "No hay receta configurada." });

        // Por cada material, cuántas unidades alcanza su stock actual -> el mínimo de todos manda
        var unidadesPosibles = receta
            .Select(r => r.MateriaPrima!.Stock / r.CantidadRequerida)
            .DefaultIfEmpty(0)
            .Min();

        return Ok(new { unidadesPosibles = (int)Math.Floor(unidadesPosibles) });
    }

    // POST api/produccion -> fabrica N unidades, descuenta materia prima, aumenta stock de producto
    [HttpPost]
    public async Task<IActionResult> Producir(ProduccionDto dto)
    {
        if (dto.Cantidad <= 0)
            return BadRequest("La cantidad a producir debe ser mayor a 0.");

        var producto = await _context.Productos.FirstOrDefaultAsync();
        if (producto is null) return BadRequest("No hay producto configurado.");

        var receta = await _context.Recetas.Include(r => r.MateriaPrima).ToListAsync();
        if (receta.Count == 0) return BadRequest("No hay receta configurada.");

        // --- Validar que alcance la materia prima ANTES de tocar nada ---
        var faltantes = new List<string>();

        foreach (var item in receta)
        {
            var requerido = item.CantidadRequerida * dto.Cantidad;
            if (item.MateriaPrima!.Stock < requerido)
            {
                faltantes.Add($"{item.MateriaPrima.Nombre}: se necesitan {requerido} {item.MateriaPrima.UnidadMedida}, hay {item.MateriaPrima.Stock}");
            }
        }

        if (faltantes.Count > 0)
        {
            return BadRequest(new
            {
                mensaje = "No hay suficiente materia prima para esta producción. Realiza compras para reabastecer.",
                faltantes
            });
        }

        // --- Todo alcanza: descontar materia prima y sumar producto terminado ---
        foreach (var item in receta)
        {
            item.MateriaPrima!.Stock -= item.CantidadRequerida * dto.Cantidad;
        }

        producto.Stock += dto.Cantidad;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            mensaje = $"Se produjeron {dto.Cantidad} unidades de {producto.Nombre}.",
            nuevoStockProducto = producto.Stock
        });
    }
}