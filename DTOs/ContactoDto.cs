namespace Nexora.Api.DTOs;

public class ContactoDto
{
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Asunto { get; set; } = null!;
    public string Mensaje { get; set; } = null!;
}