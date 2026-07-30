using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Api.Migrations
{
    /// <inheritdoc />
    public partial class SimplificaCotizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesCotizacion");

            migrationBuilder.RenameColumn(
                name: "EmailContacto",
                table: "Cotizaciones",
                newName: "TipoTransporte");

            migrationBuilder.AddColumn<bool>(
                name: "Capacitacion",
                table: "Cotizaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CiudadRegion",
                table: "Cotizaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CostoUnitario",
                table: "Cotizaciones",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Cotizaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "InstalacionIncluida",
                table: "Cotizaciones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Iva",
                table: "Cotizaciones",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "NombreEmpresa",
                table: "Cotizaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumeroUnidades",
                table: "Cotizaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "Cotizaciones",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Cotizaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacitacion",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "CiudadRegion",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "CostoUnitario",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "InstalacionIncluida",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Iva",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "NombreEmpresa",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "NumeroUnidades",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Cotizaciones");

            migrationBuilder.RenameColumn(
                name: "TipoTransporte",
                table: "Cotizaciones",
                newName: "EmailContacto");

            migrationBuilder.CreateTable(
                name: "DetallesCotizacion",
                columns: table => new
                {
                    IdDetalleCotizacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCotizacion = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PrecioCalculado = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesCotizacion", x => x.IdDetalleCotizacion);
                    table.ForeignKey(
                        name: "FK_DetallesCotizacion_Cotizaciones_IdCotizacion",
                        column: x => x.IdCotizacion,
                        principalTable: "Cotizaciones",
                        principalColumn: "IdCotizacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesCotizacion_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCotizacion_IdCotizacion",
                table: "DetallesCotizacion",
                column: "IdCotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCotizacion_IdProducto",
                table: "DetallesCotizacion",
                column: "IdProducto");
        }
    }
}
