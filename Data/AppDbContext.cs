using Microsoft.EntityFrameworkCore;
using Nexora.Api.Models;

namespace Nexora.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Proveedor> Proveedores => Set<Proveedor>();
    public DbSet<MateriaPrima> MateriasPrimas => Set<MateriaPrima>();
    public DbSet<Compra> Compras => Set<Compra>();
    public DbSet<DetalleCompra> DetallesCompra => Set<DetalleCompra>();
    public DbSet<Receta> Recetas => Set<Receta>();
    public DbSet<Comentario> Comentarios => Set<Comentario>();
    public DbSet<Cotizacion> Cotizaciones => Set<Cotizacion>();
    public DbSet<DetalleCotizacion> DetallesCotizacion => Set<DetalleCotizacion>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();
    public DbSet<PreguntaFrecuente> PreguntasFrecuentes => Set<PreguntaFrecuente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --- Llaves primarias explícitas (nuestra convención IdXxx no la detecta EF solo) ---
        modelBuilder.Entity<Usuario>().HasKey(e => e.IdUsuario);
        modelBuilder.Entity<Producto>().HasKey(e => e.IdProducto);
        modelBuilder.Entity<Proveedor>().HasKey(e => e.IdProveedor);
        modelBuilder.Entity<MateriaPrima>().HasKey(e => e.IdMateriaPrima);
        modelBuilder.Entity<Compra>().HasKey(e => e.IdCompra);
        modelBuilder.Entity<DetalleCompra>().HasKey(e => e.IdDetalleCompra);
        modelBuilder.Entity<Receta>().HasKey(e => e.IdReceta);
        modelBuilder.Entity<Comentario>().HasKey(e => e.IdComentario);
        modelBuilder.Entity<Cotizacion>().HasKey(e => e.IdCotizacion);
        modelBuilder.Entity<DetalleCotizacion>().HasKey(e => e.IdDetalleCotizacion);
        modelBuilder.Entity<Pedido>().HasKey(e => e.IdPedido);
        modelBuilder.Entity<DetallePedido>().HasKey(e => e.IdDetallePedido);
        modelBuilder.Entity<PreguntaFrecuente>().HasKey(e => e.IdFaq);

        // --- Precisión decimal explícita ---
        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio).HasColumnType("decimal(10,2)");

        modelBuilder.Entity<MateriaPrima>()
            .Property(m => m.CostoUnitario).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<MateriaPrima>()
            .Property(m => m.Stock).HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Compra>()
            .Property(c => c.Total).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<DetalleCompra>()
            .Property(d => d.Cantidad).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<DetalleCompra>()
            .Property(d => d.CostoUnitario).HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Receta>()
            .Property(r => r.CantidadRequerida).HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Cotizacion>()
            .Property(c => c.Total).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<DetalleCotizacion>()
            .Property(d => d.Cantidad).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<DetalleCotizacion>()
            .Property(d => d.PrecioCalculado).HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Pedido>()
            .Property(p => p.Total).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<DetallePedido>()
            .Property(d => d.Cantidad).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<DetallePedido>()
            .Property(d => d.PrecioUnitario).HasColumnType("decimal(10,2)");

        base.OnModelCreating(modelBuilder);
    }
}