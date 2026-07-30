namespace Nexora.Api.Models;

public class Producto
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public decimal Precio { get; set; }
    public string? Imagen { get; set; }
    public decimal PorcentajeEnsamblaje { get; set; } = 15;
}