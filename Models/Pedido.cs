namespace Nexora.Api.Models;

public class Pedido
{
    public int IdPedido { get; set; }

    public int IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public string Estado { get; set; } = "Completado";

    public List<DetallePedido> Detalles { get; set; } = new();
}