namespace Nexora.Api.DTOs;

public class ProveedorDto
{
    public string RazonSocial { get; set; } = null!;
    public string? Contacto { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
}