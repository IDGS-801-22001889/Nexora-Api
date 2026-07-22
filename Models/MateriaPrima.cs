namespace Nexora.Api.Models;

public class MateriaPrima
{
    public int IdMateriaPrima { get; set; }
    public string Nombre { get; set; } = null!;
    public string UnidadMedida { get; set; } = null!;
    public decimal CostoUnitario { get; set; }
    public decimal Stock { get; set; }
}