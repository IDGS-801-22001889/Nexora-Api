namespace Nexora.Api.DTOs;

public class PedidoDto
{
    public int IdCotizacion { get; set; }
    public string MetodoPago { get; set; } = null!;
}

public class GestionarPedidoDto
{
    public string Estado { get; set; } = null!;
    public string? MensajeAdmin { get; set; }
}