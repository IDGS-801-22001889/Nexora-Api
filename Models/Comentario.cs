namespace Nexora.Api.Models;

public class Comentario
{
    public int IdComentario { get; set; }

    public int IdProducto { get; set; }
    public Producto? Producto { get; set; }

    public int IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    public string Texto { get; set; } = null!;
    public int Calificacion { get; set; } // 1 a 5
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string Estado { get; set; } = "Pendiente"; // Pendiente | Revisado
}