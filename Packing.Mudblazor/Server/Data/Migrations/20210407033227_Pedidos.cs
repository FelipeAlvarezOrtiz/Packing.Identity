using Microsoft.EntityFrameworkCore.Migrations;

namespace Packing.Mudblazor.Server.Data.Migrations
{
    public partial class Pedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosPedidos",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEstado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosPedidos", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstadoIdEstado = table.Column<int>(type: "int", nullable: true),
                    FechaSolicitud = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaUltimaModificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpresaSolicitanteIdEmpresa = table.Column<int>(type: "int", nullable: true),
                    UserNameSolicitante = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK_Pedidos_Empresas_EmpresaSolicitanteIdEmpresa",
                        column: x => x.EmpresaSolicitanteIdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_EstadosPedidos_EstadoIdEstado",
                        column: x => x.EstadoIdEstado,
                        principalTable: "EstadosPedidos",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallesPedidos",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoCabeceraIdPedido = table.Column<int>(type: "int", nullable: true),
                    ProductoEnDetalleIdProducto = table.Column<int>(type: "int", nullable: true),
                    CantidadProducto = table.Column<int>(type: "int", nullable: false),
                    CantidadesUnitarias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesPedidos", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_DetallesPedidos_Pedidos_PedidoCabeceraIdPedido",
                        column: x => x.PedidoCabeceraIdPedido,
                        principalTable: "Pedidos",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallesPedidos_Productos_ProductoEnDetalleIdProducto",
                        column: x => x.ProductoEnDetalleIdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_PedidoCabeceraIdPedido",
                table: "DetallesPedidos",
                column: "PedidoCabeceraIdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedidos_ProductoEnDetalleIdProducto",
                table: "DetallesPedidos",
                column: "ProductoEnDetalleIdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EmpresaSolicitanteIdEmpresa",
                table: "Pedidos",
                column: "EmpresaSolicitanteIdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_EstadoIdEstado",
                table: "Pedidos",
                column: "EstadoIdEstado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesPedidos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "EstadosPedidos");
        }
    }
}
