namespace Nexora.Api.Models;

public class Documentacion
{
    public int IdDocumento { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string TipoArchivo { get; set; } = null!; // "Documento" | "Imagen" | "Video"
    public string NombreArchivo { get; set; } = null!; // nombre original
    public string RutaArchivo { get; set; } = null!;   // ruta relativa donde se guardó
    public DateTime FechaSubida { get; set; } = DateTime.UtcNow;
}