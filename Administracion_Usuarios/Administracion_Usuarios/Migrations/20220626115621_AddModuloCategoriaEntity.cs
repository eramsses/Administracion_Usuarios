using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administracion_Usuarios.Migrations
{
    public partial class AddModuloCategoriaEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModuloCategoriaId",
                table: "Modulos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ModuloCategoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuloCategoria", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_ModuloCategoriaId",
                table: "Modulos",
                column: "ModuloCategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modulos_ModuloCategoria_ModuloCategoriaId",
                table: "Modulos",
                column: "ModuloCategoriaId",
                principalTable: "ModuloCategoria",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modulos_ModuloCategoria_ModuloCategoriaId",
                table: "Modulos");

            migrationBuilder.DropTable(
                name: "ModuloCategoria");

            migrationBuilder.DropIndex(
                name: "IX_Modulos_ModuloCategoriaId",
                table: "Modulos");

            migrationBuilder.DropColumn(
                name: "ModuloCategoriaId",
                table: "Modulos");
        }
    }
}
