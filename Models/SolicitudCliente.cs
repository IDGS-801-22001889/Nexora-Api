namespace Nexora.Api.Models;

public class SolicitudCliente
{
    public int IdSolicitud { get; set; }
    public string NombreEmpresa { get; set; } = null!;
    public string NombreContacto { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string? Mensaje { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string Estado { get; set; } = "Pendiente"; // Pendiente | Procesada | Rechazada
}