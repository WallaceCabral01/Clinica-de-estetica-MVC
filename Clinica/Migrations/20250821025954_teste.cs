using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica.Migrations
{
    /// <inheritdoc />
    public partial class teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Pacientes_PacienteId",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_PacienteId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "PacienteId",
                table: "Consultas");

            migrationBuilder.RenameColumn(
                name: "PacienteId",
                table: "Pacientes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Procedimento",
                table: "Consultas",
                newName: "Tratamento");

            migrationBuilder.RenameColumn(
                name: "ConsultaId",
                table: "Consultas",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "PacienteConsultas",
                columns: table => new
                {
                    PacienteId = table.Column<int>(type: "int", nullable: false),
                    ConsultaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacienteConsultas", x => new { x.PacienteId, x.ConsultaId });
                    table.ForeignKey(
                        name: "FK_PacienteConsultas_Consultas_ConsultaId",
                        column: x => x.ConsultaId,
                        principalTable: "Consultas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacienteConsultas_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacienteConsultas_ConsultaId",
                table: "PacienteConsultas",
                column: "ConsultaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacienteConsultas");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pacientes",
                newName: "PacienteId");

            migrationBuilder.RenameColumn(
                name: "Tratamento",
                table: "Consultas",
                newName: "Procedimento");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Consultas",
                newName: "ConsultaId");

            migrationBuilder.AddColumn<int>(
                name: "PacienteId",
                table: "Consultas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PacienteId",
                table: "Consultas",
                column: "PacienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Pacientes_PacienteId",
                table: "Consultas",
                column: "PacienteId",
                principalTable: "Pacientes",
                principalColumn: "PacienteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
