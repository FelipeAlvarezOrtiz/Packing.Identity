using Microsoft.EntityFrameworkCore.Migrations;

namespace Packing.Mudblazor.Server.Data.Migrations
{
    public partial class ImplementacionProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formatos",
                columns: table => new
                {
                    IdFormato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreFormato = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formatos", x => x.IdFormato);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    IdGrupo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreGrupo = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.IdGrupo);
                });

            migrationBuilder.CreateTable(
                name: "Presentaciones",
                columns: table => new
                {
                    IdPresentacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePresentacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentaciones", x => x.IdPresentacion);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FormatoIdFormato = table.Column<int>(type: "int", nullable: true),
                    PresentacionIdPresentacion = table.Column<int>(type: "int", nullable: true),
                    Vigente = table.Column<bool>(type: "bit", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrupoProductoIdGrupo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_Productos_Formatos_FormatoIdFormato",
                        column: x => x.FormatoIdFormato,
                        principalTable: "Formatos",
                        principalColumn: "IdFormato",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_Grupos_GrupoProductoIdGrupo",
                        column: x => x.GrupoProductoIdGrupo,
                        principalTable: "Grupos",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Productos_Presentaciones_PresentacionIdPresentacion",
                        column: x => x.PresentacionIdPresentacion,
                        principalTable: "Presentaciones",
                        principalColumn: "IdPresentacion",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productos_FormatoIdFormato",
                table: "Productos",
                column: "FormatoIdFormato");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_GrupoProductoIdGrupo",
                table: "Productos",
                column: "GrupoProductoIdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_PresentacionIdPresentacion",
                table: "Productos",
                column: "PresentacionIdPresentacion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Formatos");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Presentaciones");
        }
    }
}
