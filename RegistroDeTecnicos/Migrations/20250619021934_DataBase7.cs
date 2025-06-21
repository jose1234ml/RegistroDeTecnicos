using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class DataBase7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VentaDetalleId",
                table: "VentasDetalle",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VentaDetalleId",
                table: "VentasDetalle");
        }
    }
}
