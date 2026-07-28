using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexora.Api.Data;
using Nexora.Api.DTOs;
using Nexora.Api.Models;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductoController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/producto -> pública, devuelve el único producto (o null si no existe aún)
    [HttpGet]
    public async Task<ActionResult<Producto>> Get()
    {
        var producto = await _context.Productos.FirstOrDefaultAsync();
        if (producto is null) return NotFound();

        return Ok(producto);
    }

    // POST api/producto -> solo administrador, lo crea si no existe
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<Producto>> Create(ProductoDto dto)
    {
        var yaExiste = await _context.Productos.AnyAsync();
        if (yaExiste)
            return BadRequest("Ya existe un producto registrado. Usa editar en vez de crear otro.");

        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Imagen = dto.Imagen
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return Ok(producto);
    }

    // PUT api/producto/5 -> solo administrador
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Update(int id, ProductoDto dto)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto is null) return NotFound();

        producto.Nombre = dto.Nombre;
        producto.Descripcion = dto.Descripcion;
        producto.Precio = dto.Precio;
        producto.Imagen = dto.Imagen;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}