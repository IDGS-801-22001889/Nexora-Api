using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Api.Migrations
{
    /// <inheritdoc />
    public partial class PedidoDesdeCotizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCotizacion",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Cotizaciones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdCotizacion",
                table: "Pedidos",
                column: "IdCotizacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Cotizaciones_IdCotizacion",
                table: "Pedidos",
                column: "IdCotizacion",
                principalTable: "Cotizaciones",
                principalColumn: "IdCotizacion",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Cotizaciones_IdCotizacion",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdCotizacion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "IdCotizacion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Cotizaciones");
        }
    }
}
