using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class ticket4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TiempoInvertido",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiempoInvertido",
                table: "Tickets");
        }
    }
}
