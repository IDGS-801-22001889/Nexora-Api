using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Api.Migrations
{
    /// <inheritdoc />
    public partial class CorrigeRelacionesExplicitas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedores_ProveedorIdProveedor",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Cotizaciones_Usuarios_UsuarioIdUsuario",
                table: "Cotizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCompra_Compras_CompraIdCompra",
                table: "DetallesCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCompra_MateriasPrimas_MateriaPrimaIdMateriaPrima",
                table: "DetallesCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCotizacion_Cotizaciones_CotizacionIdCotizacion",
                table: "DetallesCotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCotizacion_Productos_ProductoIdProducto",
                table: "DetallesCotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedido_Pedidos_PedidoIdPedido",
                table: "DetallesPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedido_Productos_ProductoIdProducto",
                table: "DetallesPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioIdUsuario",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_MateriasPrimas_MateriaPrimaIdMateriaPrima",
                table: "Recetas");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_Productos_ProductoIdProducto",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_MateriaPrimaIdMateriaPrima",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_ProductoIdProducto",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_UsuarioIdUsuario",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPedido_PedidoIdPedido",
                table: "DetallesPedido");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPedido_ProductoIdProducto",
                table: "DetallesPedido");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCotizacion_CotizacionIdCotizacion",
                table: "DetallesCotizacion");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCotizacion_ProductoIdProducto",
                table: "DetallesCotizacion");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCompra_CompraIdCompra",
                table: "DetallesCompra");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCompra_MateriaPrimaIdMateriaPrima",
                table: "DetallesCompra");

            migrationBuilder.DropIndex(
                name: "IX_Cotizaciones_UsuarioIdUsuario",
                table: "Cotizaciones");

            migrationBuilder.DropIndex(
                name: "IX_Compras_ProveedorIdProveedor",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "MateriaPrimaIdMateriaPrima",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "ProductoIdProducto",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "UsuarioIdUsuario",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "PedidoIdPedido",
                table: "DetallesPedido");

            migrationBuilder.DropColumn(
                name: "ProductoIdProducto",
                table: "DetallesPedido");

            migrationBuilder.DropColumn(
                name: "CotizacionIdCotizacion",
                table: "DetallesCotizacion");

            migrationBuilder.DropColumn(
                name: "ProductoIdProducto",
                table: "DetallesCotizacion");

            migrationBuilder.DropColumn(
                name: "CompraIdCompra",
                table: "DetallesCompra");

            migrationBuilder.DropColumn(
                name: "MateriaPrimaIdMateriaPrima",
                table: "DetallesCompra");

            migrationBuilder.DropColumn(
                name: "UsuarioIdUsuario",
                table: "Cotizaciones");

            migrationBuilder.DropColumn(
                name: "ProveedorIdProveedor",
                table: "Compras");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_IdMateriaPrima",
                table: "Recetas",
                column: "IdMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_IdProducto",
                table: "Recetas",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdUsuario",
                table: "Pedidos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedido_IdPedido",
                table: "DetallesPedido",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedido_IdProducto",
                table: "DetallesPedido",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCotizacion_IdCotizacion",
                table: "DetallesCotizacion",
                column: "IdCotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCotizacion_IdProducto",
                table: "DetallesCotizacion",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompra_IdCompra",
                table: "DetallesCompra",
                column: "IdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompra_IdMateriaPrima",
                table: "DetallesCompra",
                column: "IdMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_IdUsuario",
                table: "Cotizaciones",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdProveedor",
                table: "Compras",
                column: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedores_IdProveedor",
                table: "Compras",
                column: "IdProveedor",
                principalTable: "Proveedores",
                principalColumn: "IdProveedor",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cotizaciones_Usuarios_IdUsuario",
                table: "Cotizaciones",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCompra_Compras_IdCompra",
                table: "DetallesCompra",
                column: "IdCompra",
                principalTable: "Compras",
                principalColumn: "IdCompra",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCompra_MateriasPrimas_IdMateriaPrima",
                table: "DetallesCompra",
                column: "IdMateriaPrima",
                principalTable: "MateriasPrimas",
                principalColumn: "IdMateriaPrima",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCotizacion_Cotizaciones_IdCotizacion",
                table: "DetallesCotizacion",
                column: "IdCotizacion",
                principalTable: "Cotizaciones",
                principalColumn: "IdCotizacion",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCotizacion_Productos_IdProducto",
                table: "DetallesCotizacion",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedido_Pedidos_IdPedido",
                table: "DetallesPedido",
                column: "IdPedido",
                principalTable: "Pedidos",
                principalColumn: "IdPedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedido_Productos_IdProducto",
                table: "DetallesPedido",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_IdUsuario",
                table: "Pedidos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_MateriasPrimas_IdMateriaPrima",
                table: "Recetas",
                column: "IdMateriaPrima",
                principalTable: "MateriasPrimas",
                principalColumn: "IdMateriaPrima",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_Productos_IdProducto",
                table: "Recetas",
                column: "IdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Proveedores_IdProveedor",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Cotizaciones_Usuarios_IdUsuario",
                table: "Cotizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCompra_Compras_IdCompra",
                table: "DetallesCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCompra_MateriasPrimas_IdMateriaPrima",
                table: "DetallesCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCotizacion_Cotizaciones_IdCotizacion",
                table: "DetallesCotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesCotizacion_Productos_IdProducto",
                table: "DetallesCotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedido_Pedidos_IdPedido",
                table: "DetallesPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallesPedido_Productos_IdProducto",
                table: "DetallesPedido");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Usuarios_IdUsuario",
                table: "Pedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_MateriasPrimas_IdMateriaPrima",
                table: "Recetas");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_Productos_IdProducto",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_IdMateriaPrima",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_IdProducto",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Pedidos_IdUsuario",
                table: "Pedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPedido_IdPedido",
                table: "DetallesPedido");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPedido_IdProducto",
                table: "DetallesPedido");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCotizacion_IdCotizacion",
                table: "DetallesCotizacion");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCotizacion_IdProducto",
                table: "DetallesCotizacion");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCompra_IdCompra",
                table: "DetallesCompra");

            migrationBuilder.DropIndex(
                name: "IX_DetallesCompra_IdMateriaPrima",
                table: "DetallesCompra");

            migrationBuilder.DropIndex(
                name: "IX_Cotizaciones_IdUsuario",
                table: "Cotizaciones");

            migrationBuilder.DropIndex(
                name: "IX_Compras_IdProveedor",
                table: "Compras");

            migrationBuilder.AddColumn<int>(
                name: "MateriaPrimaIdMateriaPrima",
                table: "Recetas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductoIdProducto",
                table: "Recetas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioIdUsuario",
                table: "Pedidos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PedidoIdPedido",
                table: "DetallesPedido",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductoIdProducto",
                table: "DetallesPedido",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CotizacionIdCotizacion",
                table: "DetallesCotizacion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductoIdProducto",
                table: "DetallesCotizacion",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompraIdCompra",
                table: "DetallesCompra",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MateriaPrimaIdMateriaPrima",
                table: "DetallesCompra",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioIdUsuario",
                table: "Cotizaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProveedorIdProveedor",
                table: "Compras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_MateriaPrimaIdMateriaPrima",
                table: "Recetas",
                column: "MateriaPrimaIdMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_ProductoIdProducto",
                table: "Recetas",
                column: "ProductoIdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioIdUsuario",
                table: "Pedidos",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedido_PedidoIdPedido",
                table: "DetallesPedido",
                column: "PedidoIdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedido_ProductoIdProducto",
                table: "DetallesPedido",
                column: "ProductoIdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCotizacion_CotizacionIdCotizacion",
                table: "DetallesCotizacion",
                column: "CotizacionIdCotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCotizacion_ProductoIdProducto",
                table: "DetallesCotizacion",
                column: "ProductoIdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompra_CompraIdCompra",
                table: "DetallesCompra",
                column: "CompraIdCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesCompra_MateriaPrimaIdMateriaPrima",
                table: "DetallesCompra",
                column: "MateriaPrimaIdMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_UsuarioIdUsuario",
                table: "Cotizaciones",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ProveedorIdProveedor",
                table: "Compras",
                column: "ProveedorIdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Proveedores_ProveedorIdProveedor",
                table: "Compras",
                column: "ProveedorIdProveedor",
                principalTable: "Proveedores",
                principalColumn: "IdProveedor");

            migrationBuilder.AddForeignKey(
                name: "FK_Cotizaciones_Usuarios_UsuarioIdUsuario",
                table: "Cotizaciones",
                column: "UsuarioIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCompra_Compras_CompraIdCompra",
                table: "DetallesCompra",
                column: "CompraIdCompra",
                principalTable: "Compras",
                principalColumn: "IdCompra");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCompra_MateriasPrimas_MateriaPrimaIdMateriaPrima",
                table: "DetallesCompra",
                column: "MateriaPrimaIdMateriaPrima",
                principalTable: "MateriasPrimas",
                principalColumn: "IdMateriaPrima");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCotizacion_Cotizaciones_CotizacionIdCotizacion",
                table: "DetallesCotizacion",
                column: "CotizacionIdCotizacion",
                principalTable: "Cotizaciones",
                principalColumn: "IdCotizacion");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesCotizacion_Productos_ProductoIdProducto",
                table: "DetallesCotizacion",
                column: "ProductoIdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedido_Pedidos_PedidoIdPedido",
                table: "DetallesPedido",
                column: "PedidoIdPedido",
                principalTable: "Pedidos",
                principalColumn: "IdPedido");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesPedido_Productos_ProductoIdProducto",
                table: "DetallesPedido",
                column: "ProductoIdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Usuarios_UsuarioIdUsuario",
                table: "Pedidos",
                column: "UsuarioIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_MateriasPrimas_MateriaPrimaIdMateriaPrima",
                table: "Recetas",
                column: "MateriaPrimaIdMateriaPrima",
                principalTable: "MateriasPrimas",
                principalColumn: "IdMateriaPrima");

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_Productos_ProductoIdProducto",
                table: "Recetas",
                column: "ProductoIdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto");
        }
    }
}
