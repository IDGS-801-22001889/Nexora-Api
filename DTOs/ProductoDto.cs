namespace Nexora.Api.DTOs;

public class ProductoDto
{
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public decimal Precio { get; set; }
    public string? Imagen { get; set; }
}