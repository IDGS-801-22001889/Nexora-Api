namespace Nexora.Api.Models;

public class PreguntaFrecuente
{
    public int IdFaq { get; set; }
    public string Pregunta { get; set; } = null!;
    public string Respuesta { get; set; } = null!;
    public int Orden { get; set; }
}