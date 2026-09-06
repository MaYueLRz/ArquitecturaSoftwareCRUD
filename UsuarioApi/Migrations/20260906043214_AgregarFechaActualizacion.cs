using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuarioApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarFechaActualizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "Usuarios");
        }
    }
}
