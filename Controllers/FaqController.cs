using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.DTOs;
using Nexora.Api.Models;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FaqController : ControllerBase
{
    private readonly AppDbContext _context;

    public FaqController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/faq  -> pública, cualquier visitante puede ver las preguntas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PreguntaFrecuente>>> GetAll()
    {
        var faqs = await _context.PreguntasFrecuentes
            .OrderBy(f => f.Orden)
            .ToListAsync();

        return Ok(faqs);
    }

    // GET api/faq/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PreguntaFrecuente>> GetById(int id)
    {
        var faq = await _context.PreguntasFrecuentes.FindAsync(id);
        if (faq is null) return NotFound();
        return Ok(faq);
    }

    // POST api/faq  -> solo administradores
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<PreguntaFrecuente>> Create(FaqDto dto)
    {
        var faq = new PreguntaFrecuente
        {
            Pregunta = dto.Pregunta,
            Respuesta = dto.Respuesta,
            Orden = dto.Orden
        };

        _context.PreguntasFrecuentes.Add(faq);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = faq.IdFaq }, faq);
    }

    // PUT api/faq/5  -> solo administradores
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Update(int id, FaqDto dto)
    {
        var faq = await _context.PreguntasFrecuentes.FindAsync(id);
        if (faq is null) return NotFound();

        faq.Pregunta = dto.Pregunta;
        faq.Respuesta = dto.Respuesta;
        faq.Orden = dto.Orden;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/faq/5  -> solo administradores
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        var faq = await _context.PreguntasFrecuentes.FindAsync(id);
        if (faq is null) return NotFound();

        _context.PreguntasFrecuentes.Remove(faq);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}