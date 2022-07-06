using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administracion_Usuarios.Migrations
{
    public partial class CambioEnEstadoTblRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoEnum",
                table: "Roles");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "EstadoEnum",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
