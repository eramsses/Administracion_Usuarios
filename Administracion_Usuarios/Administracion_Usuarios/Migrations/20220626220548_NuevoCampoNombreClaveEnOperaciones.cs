using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administracion_Usuarios.Migrations
{
    public partial class NuevoCampoNombreClaveEnOperaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operaciones_Nombre",
                table: "Operaciones");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Operaciones",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "NombreClave",
                table: "Operaciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Modulos",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "ModuloCategoria",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_NombreClave",
                table: "Operaciones",
                column: "NombreClave",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuloCategoria_Nombre",
                table: "ModuloCategoria",
                column: "Nombre",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Operaciones_NombreClave",
                table: "Operaciones");

            migrationBuilder.DropIndex(
                name: "IX_ModuloCategoria_Nombre",
                table: "ModuloCategoria");

            migrationBuilder.DropColumn(
                name: "NombreClave",
                table: "Operaciones");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Operaciones",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Modulos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "ModuloCategoria",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.CreateIndex(
                name: "IX_Operaciones_Nombre",
                table: "Operaciones",
                column: "Nombre",
                unique: true);
        }
    }
}
