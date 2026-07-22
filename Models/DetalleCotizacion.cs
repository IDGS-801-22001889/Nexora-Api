namespace Nexora.Api.Models;

public class DetalleCotizacion
{
    public int IdDetalleCotizacion { get; set; }

    public int IdCotizacion { get; set; }
    public Cotizacion? Cotizacion { get; set; }

    public int IdProducto { get; set; }
    public Producto? Producto { get; set; }

    public decimal Cantidad { get; set; }
    public decimal PrecioCalculado { get; set; }
}