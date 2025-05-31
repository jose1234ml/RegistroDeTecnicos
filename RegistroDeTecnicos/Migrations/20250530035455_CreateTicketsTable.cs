using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class CreateTicketsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TiempoInvertido",
                table: "Tickets",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TiempoInvertido",
                table: "Tickets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
