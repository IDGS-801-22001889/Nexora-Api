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
public class MateriaPrimaController : ControllerBase
{
    private readonly AppDbContext _context;

    public MateriaPrimaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MateriaPrima>>> GetAll()
    {
        var materias = await _context.MateriasPrimas
            .OrderBy(m => m.Nombre)
            .ToListAsync();

        return Ok(materias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MateriaPrima>> GetById(int id)
    {
        var materia = await _context.MateriasPrimas.FindAsync(id);
        if (materia is null) return NotFound();

        return Ok(materia);
    }

    [HttpPost]
    public async Task<ActionResult<MateriaPrima>> Create(MateriaPrimaDto dto)
    {
        var materia = new MateriaPrima
        {
            Nombre = dto.Nombre,
            UnidadMedida = dto.UnidadMedida,
            CostoUnitario = dto.CostoUnitario,
            Stock = dto.Stock
        };

        _context.MateriasPrimas.Add(materia);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = materia.IdMateriaPrima }, materia);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MateriaPrimaDto dto)
    {
        var materia = await _context.MateriasPrimas.FindAsync(id);
        if (materia is null) return NotFound();

        // Nombre y unidad se pueden editar libremente.
        // Costo y Stock NO se editan aquí a mano una vez que ya se usó en compras/recetas,
        // para no perder la trazabilidad del Promedio Ponderado.
        materia.Nombre = dto.Nombre;
        materia.UnidadMedida = dto.UnidadMedida;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var materia = await _context.MateriasPrimas.FindAsync(id);
        if (materia is null) return NotFound();

        var enUso = await _context.DetallesCompra.AnyAsync(d => d.IdMateriaPrima == id)
                 || await _context.Recetas.AnyAsync(r => r.IdMateriaPrima == id);

        if (enUso)
            return BadRequest("No se puede eliminar: esta materia prima ya está en uso en compras o recetas.");

        _context.MateriasPrimas.Remove(materia);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}