namespace Nexora.Api.Models;

public class Cotizacion
{
    public int IdCotizacion { get; set; }

    public int? IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    public string NombreEmpresa { get; set; } = null!;
    public string NombreContacto { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Telefono { get; set; } = null!;

    public int NumeroUnidades { get; set; }
    public string TipoTransporte { get; set; } = null!; // "Carga" | "Pasajeros"
    public string CiudadRegion { get; set; } = null!;

    public bool InstalacionIncluida { get; set; }
    public bool Capacitacion { get; set; }

    public decimal CostoUnitario { get; set; } // snapshot del costo al momento de cotizar
    public decimal Subtotal { get; set; }
    public decimal Iva { get; set; }
    public decimal Total { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}