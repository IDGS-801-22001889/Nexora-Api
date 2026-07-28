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
public class ProveedorController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProveedorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proveedor>>> GetAll()
    {
        var proveedores = await _context.Proveedores
            .OrderBy(p => p.RazonSocial)
            .ToListAsync();

        return Ok(proveedores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Proveedor>> GetById(int id)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);
        if (proveedor is null) return NotFound();

        return Ok(proveedor);
    }

    [HttpPost]
    public async Task<ActionResult<Proveedor>> Create(ProveedorDto dto)
    {
        var proveedor = new Proveedor
        {
            RazonSocial = dto.RazonSocial,
            Contacto = dto.Contacto,
            Telefono = dto.Telefono,
            Email = dto.Email,
            Direccion = dto.Direccion
        };

        _context.Proveedores.Add(proveedor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = proveedor.IdProveedor }, proveedor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProveedorDto dto)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);
        if (proveedor is null) return NotFound();

        proveedor.RazonSocial = dto.RazonSocial;
        proveedor.Contacto = dto.Contacto;
        proveedor.Telefono = dto.Telefono;
        proveedor.Email = dto.Email;
        proveedor.Direccion = dto.Direccion;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var proveedor = await _context.Proveedores.FindAsync(id);
        if (proveedor is null) return NotFound();

        var tieneCompras = await _context.Compras.AnyAsync(c => c.IdProveedor == id);
        if (tieneCompras)
            return BadRequest("No se puede eliminar: este proveedor tiene compras registradas.");

        _context.Proveedores.Remove(proveedor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}