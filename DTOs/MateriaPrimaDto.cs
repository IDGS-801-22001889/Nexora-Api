namespace Nexora.Api.DTOs;

public class MateriaPrimaDto
{
    public string Nombre { get; set; } = null!;
    public string UnidadMedida { get; set; } = null!;
    public decimal CostoUnitario { get; set; }
    public decimal Stock { get; set; }
}