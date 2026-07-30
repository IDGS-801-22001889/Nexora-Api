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
public class RecetaController : ControllerBase
{
    private readonly AppDbContext _context;

    public RecetaController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/receta -> lista de materia prima que compone el producto, + costo total calculado
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var producto = await _context.Productos.FirstOrDefaultAsync();
        var receta = await _context.Recetas
            .Include(r => r.MateriaPrima)
            .ToListAsync();

        var costoMateriales = receta.Sum(r => r.CantidadRequerida * (r.MateriaPrima?.CostoUnitario ?? 0));
        var porcentajeEnsamblaje = producto?.PorcentajeEnsamblaje ?? 0;
        var costoEnsamblaje = costoMateriales * (porcentajeEnsamblaje / 100);
        var costoTotal = costoMateriales + costoEnsamblaje;

        return Ok(new
        {
            items = receta.Select(r => new
            {
                r.IdReceta,
                r.IdMateriaPrima,
                nombreMateriaPrima = r.MateriaPrima!.Nombre,
                unidadMedida = r.MateriaPrima.UnidadMedida,
                costoUnitario = r.MateriaPrima.CostoUnitario,
                r.CantidadRequerida,
                subtotal = r.CantidadRequerida * r.MateriaPrima.CostoUnitario
            }),
            costoMateriales,
            porcentajeEnsamblaje,
            costoEnsamblaje,
            costoTotal
        });
    }

    // POST api/receta -> agrega una materia prima a la receta del producto
    [HttpPost]
    public async Task<IActionResult> Create(RecetaDto dto)
    {
        var producto = await _context.Productos.FirstOrDefaultAsync();
        if (producto is null) return BadRequest("No hay producto configurado.");

        var yaExiste = await _context.Recetas
            .AnyAsync(r => r.IdProducto == producto.IdProducto && r.IdMateriaPrima == dto.IdMateriaPrima);
        if (yaExiste)
            return BadRequest("Esta materia prima ya está en la receta. Edítala en vez de agregarla de nuevo.");

        var receta = new Receta
        {
            IdProducto = producto.IdProducto,
            IdMateriaPrima = dto.IdMateriaPrima,
            CantidadRequerida = dto.CantidadRequerida
        };

        _context.Recetas.Add(receta);
        await _context.SaveChangesAsync();

        return Ok(receta);
    }

    // PUT api/receta/5 -> edita la cantidad requerida
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] decimal cantidadRequerida)
    {
        var receta = await _context.Recetas.FindAsync(id);
        if (receta is null) return NotFound();

        receta.CantidadRequerida = cantidadRequerida;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE api/receta/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var receta = await _context.Recetas.FindAsync(id);
        if (receta is null) return NotFound();

        _context.Recetas.Remove(receta);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}