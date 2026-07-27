using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.DTOs;
using Nexora.Api.Models;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SolicitudesController(AppDbContext context)
    {
        _context = context;
    }

    // POST api/solicitudes -> pública, cualquier visitante puede solicitar acceso
    [HttpPost]
    public async Task<ActionResult<SolicitudCliente>> Create(SolicitudClienteDto dto)
    {
        var solicitud = new SolicitudCliente
        {
            NombreEmpresa = dto.NombreEmpresa,
            NombreContacto = dto.NombreContacto,
            Email = dto.Email,
            Telefono = dto.Telefono,
            Mensaje = dto.Mensaje
        };

        _context.SolicitudesCliente.Add(solicitud);
        await _context.SaveChangesAsync();

        return Ok(solicitud);
    }

    // GET api/solicitudes -> solo administradores
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<IEnumerable<SolicitudCliente>>> GetAll()
    {
        var solicitudes = await _context.SolicitudesCliente
            .OrderByDescending(s => s.Fecha)
            .ToListAsync();

        return Ok(solicitudes);
    }

    // PUT api/solicitudes/5/estado -> solo administradores, cambia el estado
    [HttpPut("{id}/estado")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> CambiarEstado(int id, [FromBody] string estado)
    {
        var solicitud = await _context.SolicitudesCliente.FindAsync(id);
        if (solicitud is null) return NotFound();

        solicitud.Estado = estado;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}