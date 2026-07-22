namespace Nexora.Api.Models;

public class Compra
{
    public int IdCompra { get; set; }
    public int IdProveedor { get; set; }
    public Proveedor? Proveedor { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }

    public List<DetalleCompra> Detalles { get; set; } = new();
}