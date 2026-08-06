namespace Nexora.Api.Models;

public class Pedido
{
    public int IdPedido { get; set; }

    public int IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    public int IdCotizacion { get; set; }
    public Cotizacion? Cotizacion { get; set; }

    public int Cantidad { get; set; }
    public string MetodoPago { get; set; } = null!;
    public decimal Total { get; set; }

    public string Estado { get; set; } = "En espera";
    public string? MensajeAdmin { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}