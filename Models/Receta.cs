namespace Nexora.Api.Models;

public class Receta
{
    public int IdReceta { get; set; }

    public int IdProducto { get; set; }
    public Producto? Producto { get; set; }

    public int IdMateriaPrima { get; set; }
    public MateriaPrima? MateriaPrima { get; set; }

    public decimal CantidadRequerida { get; set; }
}