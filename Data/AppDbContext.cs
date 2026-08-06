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
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PreguntaFrecuente> PreguntasFrecuentes => Set<PreguntaFrecuente>();
    public DbSet<SolicitudCliente> SolicitudesCliente => Set<SolicitudCliente>();
    public DbSet<Documentacion> Documentaciones => Set<Documentacion>();

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
        modelBuilder.Entity<Pedido>().HasKey(e => e.IdPedido);
        modelBuilder.Entity<PreguntaFrecuente>().HasKey(e => e.IdFaq);
        modelBuilder.Entity<SolicitudCliente>().HasKey(e => e.IdSolicitud);
        modelBuilder.Entity<Documentacion>().HasKey(e => e.IdDocumento);

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
        modelBuilder.Entity<Cotizacion>()
            .Property(c => c.CostoUnitario).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<Cotizacion>()
            .Property(c => c.Subtotal).HasColumnType("decimal(10,2)");
        modelBuilder.Entity<Cotizacion>()
            .Property(c => c.Iva).HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Pedido>()
            .Property(p => p.Total).HasColumnType("decimal(10,2)");

        // --- Relaciones explícitas (evita columnas FK duplicadas/fantasma) ---
        modelBuilder.Entity<Compra>()
            .HasOne(c => c.Proveedor)
            .WithMany()
            .HasForeignKey(c => c.IdProveedor);

        modelBuilder.Entity<DetalleCompra>()
            .HasOne(d => d.Compra)
            .WithMany(c => c.Detalles)
            .HasForeignKey(d => d.IdCompra);

        modelBuilder.Entity<DetalleCompra>()
            .HasOne(d => d.MateriaPrima)
            .WithMany()
            .HasForeignKey(d => d.IdMateriaPrima);

        modelBuilder.Entity<Receta>()
            .HasOne(r => r.Producto)
            .WithMany()
            .HasForeignKey(r => r.IdProducto);

        modelBuilder.Entity<Receta>()
            .HasOne(r => r.MateriaPrima)
            .WithMany()
            .HasForeignKey(r => r.IdMateriaPrima);

        modelBuilder.Entity<Cotizacion>()
            .HasOne(c => c.Usuario)
            .WithMany()
            .HasForeignKey(c => c.IdUsuario)
            .IsRequired(false);

        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(p => p.IdUsuario);

        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Cotizacion)
            .WithMany()
            .HasForeignKey(p => p.IdCotizacion);

        modelBuilder.Entity<Producto>()
            .Property(p => p.PorcentajeEnsamblaje).HasColumnType("decimal(5,2)");

        base.OnModelCreating(modelBuilder);
    }
}