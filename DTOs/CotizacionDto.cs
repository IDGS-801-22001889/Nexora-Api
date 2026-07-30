namespace Nexora.Api.DTOs;

public class CotizacionDto
{
    public string NombreEmpresa { get; set; } = null!;
    public string NombreContacto { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public int NumeroUnidades { get; set; }
    public string TipoTransporte { get; set; } = null!;
    public string CiudadRegion { get; set; } = null!;
    public bool InstalacionIncluida { get; set; }
    public bool Capacitacion { get; set; }
}