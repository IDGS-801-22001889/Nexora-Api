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
public class ComentarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public ComentarioController(AppDbContext context)
    {
        _context = context;
    }

    private int IdUsuarioActual =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // GET api/comentario -> pública, SOLO muestra los ya moderados (Revisado)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comentarios = await _context.Comentarios
            .Where(c => c.Estado == "Revisado")
            .OrderByDescending(c => c.Fecha)
            .Join(_context.Usuarios,
                c => c.IdUsuario,
                u => u.IdUsuario,
                (c, u) => new
                {
                    c.IdComentario,
                    c.Texto,
                    c.Calificacion,
                    c.Fecha,
                    NombreCliente = u.Nombre
                })
            .ToListAsync();

        var promedio = comentarios.Count > 0 ? comentarios.Average(c => c.Calificacion) : 0;

        return Ok(new
        {
            promedio = Math.Round(promedio, 1),
            total = comentarios.Count,
            comentarios
        });
    }

    // POST api/comentario -> cualquier Cliente autenticado
    [HttpPost]
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> Create(ComentarioDto dto)
    {
        if (dto.Calificacion < 1 || dto.Calificacion > 5)
            return BadRequest("La calificación debe ser entre 1 y 5.");

        var producto = await _context.Productos.FirstOrDefaultAsync();
        if (producto is null) return BadRequest("No hay producto configurado.");

        var comentario = new Comentario
        {
            IdProducto = producto.IdProducto,
            IdUsuario = IdUsuarioActual,
            Texto = dto.Texto,
            Calificacion = dto.Calificacion,
            Estado = "Pendiente" // requiere moderación del admin antes de publicarse
        };

        _context.Comentarios.Add(comentario);
        await _context.SaveChangesAsync();

        return Ok(comentario);
    }

    // GET api/comentario/admin -> solo Administrador, ve TODOS (pendientes y revisados)
    [HttpGet("admin")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetAllAdmin()
    {
        var comentarios = await _context.Comentarios
            .OrderByDescending(c => c.Fecha)
            .Join(_context.Usuarios,
                c => c.IdUsuario,
                u => u.IdUsuario,
                (c, u) => new
                {
                    c.IdComentario,
                    c.Texto,
                    c.Calificacion,
                    c.Fecha,
                    c.Estado,
                    NombreCliente = u.Nombre,
                    EmailCliente = u.Email
                })
            .ToListAsync();

        return Ok(comentarios);
    }

    // PUT api/comentario/5/estado -> solo Administrador, aprueba o rechaza
    [HttpPut("{id}/estado")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] string estado)
    {
        var comentario = await _context.Comentarios.FindAsync(id);
        if (comentario is null) return NotFound();

        comentario.Estado = estado; // "Revisado" | "Pendiente" | "Rechazado"
        await _context.SaveChangesAsync();

        return NoContent();
    }
}