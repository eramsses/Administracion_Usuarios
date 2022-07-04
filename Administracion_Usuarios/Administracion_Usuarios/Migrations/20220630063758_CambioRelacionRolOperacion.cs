using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administracion_Usuarios.Migrations
{
    public partial class CambioRelacionRolOperacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolOperaciones_Operaciones_OperacionId",
                table: "RolOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_RolOperaciones_Roles_RolId",
                table: "RolOperaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolOperaciones",
                table: "RolOperaciones");

            migrationBuilder.DropIndex(
                name: "IX_RolOperaciones_RolId",
                table: "RolOperaciones");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RolOperaciones");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "RolOperaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OperacionId",
                table: "RolOperaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolOperaciones",
                table: "RolOperaciones",
                columns: new[] { "RolId", "OperacionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RolOperaciones_Operaciones_OperacionId",
                table: "RolOperaciones",
                column: "OperacionId",
                principalTable: "Operaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolOperaciones_Roles_RolId",
                table: "RolOperaciones",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolOperaciones_Operaciones_OperacionId",
                table: "RolOperaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_RolOperaciones_Roles_RolId",
                table: "RolOperaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolOperaciones",
                table: "RolOperaciones");

            migrationBuilder.AlterColumn<int>(
                name: "OperacionId",
                table: "RolOperaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "RolOperaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RolOperaciones",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolOperaciones",
                table: "RolOperaciones",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RolOperaciones_RolId",
                table: "RolOperaciones",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolOperaciones_Operaciones_OperacionId",
                table: "RolOperaciones",
                column: "OperacionId",
                principalTable: "Operaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolOperaciones_Roles_RolId",
                table: "RolOperaciones",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
