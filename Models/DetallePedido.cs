namespace Nexora.Api.Models;

public class DetallePedido
{
    public int IdDetallePedido { get; set; }

    public int IdPedido { get; set; }
    public Pedido? Pedido { get; set; }

    public int IdProducto { get; set; }
    public Producto? Producto { get; set; }

    public decimal Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}