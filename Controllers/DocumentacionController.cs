using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.Models;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentacionController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    private static readonly string[] TiposPermitidos =
        [".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png", ".mp4", ".webm"];

    public DocumentacionController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET api/documentacion -> cualquier usuario autenticado (admin o cliente)
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Documentacion>>> GetAll()
    {
        var docs = await _context.Documentaciones
            .OrderByDescending(d => d.FechaSubida)
            .ToListAsync();

        return Ok(docs);
    }

    // POST api/documentacion -> solo administradores, sube un archivo nuevo
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<Documentacion>> Create(
        [FromForm] string titulo,
        [FromForm] string? descripcion,
        [FromForm] IFormFile archivo)
    {
        if (archivo is null || archivo.Length == 0)
            return BadRequest("Debes adjuntar un archivo.");

        var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();
        if (!TiposPermitidos.Contains(extension))
            return BadRequest("Tipo de archivo no permitido.");

        var tipoArchivo = extension switch
        {
            ".jpg" or ".jpeg" or ".png" => "Imagen",
            ".mp4" or ".webm" => "Video",
            _ => "Documento"
        };

        // Nombre único para no pisar archivos con el mismo nombre
        var nombreGuardado = $"{Guid.NewGuid()}{extension}";
        var carpeta = Path.Combine(_env.WebRootPath, "uploads", "documentacion");
        Directory.CreateDirectory(carpeta); // por si no existe aún
        var rutaCompleta = Path.Combine(carpeta, nombreGuardado);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            await archivo.CopyToAsync(stream);
        }

        var doc = new Documentacion
        {
            Titulo = titulo,
            Descripcion = descripcion,
            TipoArchivo = tipoArchivo,
            NombreArchivo = archivo.FileName,
            RutaArchivo = $"/uploads/documentacion/{nombreGuardado}"
        };

        _context.Documentaciones.Add(doc);
        await _context.SaveChangesAsync();

        return Ok(doc);
    }

    // DELETE api/documentacion/5 -> solo administradores
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        var doc = await _context.Documentaciones.FindAsync(id);
        if (doc is null) return NotFound();

        var rutaFisica = Path.Combine(_env.WebRootPath, doc.RutaArchivo.TrimStart('/'));
        if (System.IO.File.Exists(rutaFisica))
            System.IO.File.Delete(rutaFisica);

        _context.Documentaciones.Remove(doc);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}