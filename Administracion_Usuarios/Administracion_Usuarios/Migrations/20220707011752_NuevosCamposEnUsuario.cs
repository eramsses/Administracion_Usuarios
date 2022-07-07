using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administracion_Usuarios.Migrations
{
    public partial class NuevosCamposEnUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "AspNetUsers",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomRolId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImagenId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "AspNetUsers",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CustomRolId",
                table: "AspNetUsers",
                column: "CustomRolId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CustomRoles_CustomRolId",
                table: "AspNetUsers",
                column: "CustomRolId",
                principalTable: "CustomRoles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CustomRoles_CustomRolId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CustomRolId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomRolId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImagenId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "AspNetUsers");
        }
    }
}
