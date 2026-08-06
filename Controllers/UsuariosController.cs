using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.DTOs;
using Nexora.Api.Services;
using System.Security.Claims;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly EmailService _emailService;

    public UsuariosController(AppDbContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    private int IdUsuarioActual =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // GET api/usuarios/me
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var usuario = await _context.Usuarios.FindAsync(IdUsuarioActual);
        if (usuario is null) return NotFound();

        return Ok(new
        {
            idUsuario = usuario.IdUsuario,
            nombre = usuario.Nombre,
            email = usuario.Email,
            rol = usuario.Rol
        });
    }

    // PUT api/usuarios/me
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe(ActualizarPerfilDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(IdUsuarioActual);
        if (usuario is null) return NotFound();

        var emailEnUso = await _context.Usuarios
            .AnyAsync(u => u.Email == dto.Email && u.IdUsuario != IdUsuarioActual);
        if (emailEnUso)
            return BadRequest("Ese correo ya está en uso por otro usuario.");

        usuario.Nombre = dto.Nombre;
        usuario.Email = dto.Email;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // PUT api/usuarios/me/password
    [HttpPut("me/password")]
    public async Task<IActionResult> CambiarPassword(CambiarPasswordDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(IdUsuarioActual);
        if (usuario is null) return NotFound();

        if (!BCrypt.Net.BCrypt.Verify(dto.PasswordActual, usuario.PasswordHash))
            return BadRequest("La contraseña actual no es correcta.");

        if (!PasswordValidator.EsValida(dto.PasswordNueva))
            return BadRequest(PasswordValidator.MensajeError);

        usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordNueva);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET api/usuarios?rol=Cliente  -> solo administradores
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetAll([FromQuery] string? rol)
    {
        var query = _context.Usuarios.AsQueryable();

        if (!string.IsNullOrEmpty(rol))
            query = query.Where(u => u.Rol == rol);

        var usuarios = await query
            .OrderBy(u => u.Nombre)
            .Select(u => new
            {
                u.IdUsuario,
                u.Nombre,
                u.Email,
                u.Rol,
                u.Activo,
                u.FechaRegistro,
                u.CorreoEnviado
            })
            .ToListAsync();

        return Ok(usuarios);
    }

    // GET api/usuarios/5  -> solo administradores
    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetById(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null) return NotFound();

        return Ok(new
        {
            usuario.IdUsuario,
            usuario.Nombre,
            usuario.Email,
            usuario.Rol,
            usuario.Activo,
            usuario.CorreoEnviado
        });
    }

    // PUT api/usuarios/5  -> solo administradores, edita a CUALQUIER usuario
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Update(int id, ActualizarUsuarioDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null) return NotFound();

        var emailEnUso = await _context.Usuarios
            .AnyAsync(u => u.Email == dto.Email && u.IdUsuario != id);
        if (emailEnUso)
            return BadRequest("Ese correo ya está en uso por otro usuario.");

        usuario.Nombre = dto.Nombre;
        usuario.Email = dto.Email;
        usuario.Rol = dto.Rol;
        usuario.Activo = dto.Activo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/usuarios/5 -> solo administradores. Desactiva, NO borra.
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null) return NotFound();

        usuario.Activo = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST api/usuarios/5/enviar-credenciales -> solo administradores
    [HttpPost("{id}/enviar-credenciales")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> EnviarCredenciales(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null) return NotFound();

        var passwordTemporal = "Nx" + Guid.NewGuid().ToString("N").Substring(0, 8);
        usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordTemporal);

        try
        {
            _emailService.EnviarCredenciales(usuario.Nombre, usuario.Email, passwordTemporal);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"No se pudo enviar el correo: {ex.Message}");
        }

        usuario.CorreoEnviado = true;
        await _context.SaveChangesAsync();

        return NoContent();
    }

}