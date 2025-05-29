using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaIngreso = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nombres = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    Rnc = table.Column<string>(type: "TEXT", nullable: false),
                    LimiteCredito = table.Column<decimal>(type: "TEXT", nullable: false),
                    TecnicoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Clientes_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "TecnicoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Nombres",
                table: "Clientes",
                column: "Nombres",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Rnc",
                table: "Clientes",
                column: "Rnc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_TecnicoId",
                table: "Clientes",
                column: "TecnicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
