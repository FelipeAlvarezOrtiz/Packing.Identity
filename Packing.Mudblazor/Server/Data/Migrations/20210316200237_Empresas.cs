using Microsoft.EntityFrameworkCore.Migrations;

namespace Packing.Mudblazor.Server.Data.Migrations
{
    public partial class Empresas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    IdEmpresa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Vigente = table.Column<bool>(type: "bit", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RutEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionEmpresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TelefonoContacto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Giro = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PersonaContacto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.IdEmpresa);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresas");

        }
    }
}
