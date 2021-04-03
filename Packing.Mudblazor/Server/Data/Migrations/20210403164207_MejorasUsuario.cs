using Microsoft.EntityFrameworkCore.Migrations;

namespace Packing.Mudblazor.Server.Data.Migrations
{
    public partial class MejorasUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreCompleto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmpresaPertenecienteIdEmpresa",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmpresaPertenecienteIdEmpresa",
                table: "AspNetUsers",
                column: "EmpresaPertenecienteIdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaPertenecienteIdEmpresa",
                table: "AspNetUsers",
                column: "EmpresaPertenecienteIdEmpresa",
                principalTable: "Empresas",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaPertenecienteIdEmpresa",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmpresaPertenecienteIdEmpresa",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmpresaPertenecienteIdEmpresa",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "NombreCompleto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
