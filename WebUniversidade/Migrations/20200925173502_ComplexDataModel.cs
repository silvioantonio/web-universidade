using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUniversidade.Migrations
{
    public partial class ComplexDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sobrenome",
                table: "Estudante",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estudante",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Curso",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "DepartamentoId",
            //    table: "Curso",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "DepartmentID",
            //    table: "Curso",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Instrutor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 40, nullable: false),
                    Sobrenome = table.Column<string>(maxLength: 40, nullable: false),
                    DataContratacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrutor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CursoInstrutor",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false),
                    InstrutorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoInstrutor", x => new { x.CursoId, x.InstrutorId });
                    table.ForeignKey(
                        name: "FK_CursoInstrutor_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoInstrutor_Instrutor_InstrutorId",
                        column: x => x.InstrutorId,
                        principalTable: "Instrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                columns: table => new
                {
                    DepartamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: true),
                    Orcamento = table.Column<decimal>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    InstrutorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.DepartamentoId);
                    table.ForeignKey(
                        name: "FK_Departamento_Instrutor_InstrutorId",
                        column: x => x.InstrutorId,
                        principalTable: "Instrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.Sql("INSERT INTO dbo.Departamento(Nome, Orcamento, DataInicio) VALUES ('Temp', 0.00, GETDATE())");
            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Curso",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Escritorio",
                columns: table => new
                {
                    InstrutorId = table.Column<int>(nullable: false),
                    Localizacao = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escritorio", x => x.InstrutorId);
                    table.ForeignKey(
                        name: "FK_Escritorio_Instrutor_InstrutorId",
                        column: x => x.InstrutorId,
                        principalTable: "Instrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_DepartamentoId",
                table: "Curso",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoInstrutor_InstrutorId",
                table: "CursoInstrutor",
                column: "InstrutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_InstrutorId",
                table: "Departamento",
                column: "InstrutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Curso_Departamento_DepartamentoId",
                table: "Curso",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curso_Departamento_DepartamentoId",
                table: "Curso");

            migrationBuilder.DropTable(
                name: "CursoInstrutor");

            migrationBuilder.DropTable(
                name: "Departamento");

            migrationBuilder.DropTable(
                name: "Escritorio");

            migrationBuilder.DropTable(
                name: "Instrutor");

            migrationBuilder.DropIndex(
                name: "IX_Curso_DepartamentoId",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Curso");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Curso");

            migrationBuilder.AlterColumn<string>(
                name: "Sobrenome",
                table: "Estudante",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estudante",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Curso",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);
        }
    }
}
