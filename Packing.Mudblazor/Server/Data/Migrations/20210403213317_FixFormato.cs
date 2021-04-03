using Microsoft.EntityFrameworkCore.Migrations;

namespace Packing.Mudblazor.Server.Data.Migrations
{
    public partial class FixFormato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnidadesPorFormato",
                table: "Formatos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnidadesPorFormato",
                table: "Formatos");
        }
    }
}
