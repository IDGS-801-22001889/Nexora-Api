namespace Nexora.Api.Models;

public class DetalleCompra
{
    public int IdDetalleCompra { get; set; }
    public int IdCompra { get; set; }
    public Compra? Compra { get; set; }

    public int IdMateriaPrima { get; set; }
    public MateriaPrima? MateriaPrima { get; set; }

    public decimal Cantidad { get; set; }
    public decimal CostoUnitario { get; set; }
}