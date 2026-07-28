namespace Nexora.Api.DTOs;

public class DetalleCompraDto
{
    public int IdMateriaPrima { get; set; }
    public decimal Cantidad { get; set; }
    public decimal CostoUnitario { get; set; } // costo al que se compró esta vez
}

public class CompraDto
{
    public int IdProveedor { get; set; }
    public List<DetalleCompraDto> Detalles { get; set; } = new();
}