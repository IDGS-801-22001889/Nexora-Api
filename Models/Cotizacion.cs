namespace Nexora.Api.Models;

public class Cotizacion
{
    public int IdCotizacion { get; set; }

    public int? IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    public string NombreContacto { get; set; } = null!;
    public string EmailContacto { get; set; } = null!;
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }

    public List<DetalleCotizacion> Detalles { get; set; } = new();
}