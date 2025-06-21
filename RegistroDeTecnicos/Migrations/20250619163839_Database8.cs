using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroDeTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class Database8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentasDetalle_Sistemas_SistemaId",
                table: "VentasDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_VentasDetalle_Ventas_VentaId",
                table: "VentasDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VentasDetalle",
                table: "VentasDetalle");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VentasDetalle");

            migrationBuilder.RenameTable(
                name: "VentasDetalle",
                newName: "VentaDetalles");

            migrationBuilder.RenameIndex(
                name: "IX_VentasDetalle_VentaId",
                table: "VentaDetalles",
                newName: "IX_VentaDetalles_VentaId");

            migrationBuilder.RenameIndex(
                name: "IX_VentasDetalle_SistemaId",
                table: "VentaDetalles",
                newName: "IX_VentaDetalles_SistemaId");

            migrationBuilder.AlterColumn<int>(
                name: "VentaDetalleId",
                table: "VentaDetalles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VentaDetalles",
                table: "VentaDetalles",
                column: "VentaDetalleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalles_Sistemas_SistemaId",
                table: "VentaDetalles",
                column: "SistemaId",
                principalTable: "Sistemas",
                principalColumn: "SistemaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalles_Ventas_VentaId",
                table: "VentaDetalles",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalles_Sistemas_SistemaId",
                table: "VentaDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalles_Ventas_VentaId",
                table: "VentaDetalles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VentaDetalles",
                table: "VentaDetalles");

            migrationBuilder.RenameTable(
                name: "VentaDetalles",
                newName: "VentasDetalle");

            migrationBuilder.RenameIndex(
                name: "IX_VentaDetalles_VentaId",
                table: "VentasDetalle",
                newName: "IX_VentasDetalle_VentaId");

            migrationBuilder.RenameIndex(
                name: "IX_VentaDetalles_SistemaId",
                table: "VentasDetalle",
                newName: "IX_VentasDetalle_SistemaId");

            migrationBuilder.AlterColumn<int>(
                name: "VentaDetalleId",
                table: "VentasDetalle",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VentasDetalle",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VentasDetalle",
                table: "VentasDetalle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VentasDetalle_Sistemas_SistemaId",
                table: "VentasDetalle",
                column: "SistemaId",
                principalTable: "Sistemas",
                principalColumn: "SistemaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VentasDetalle_Ventas_VentaId",
                table: "VentasDetalle",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
