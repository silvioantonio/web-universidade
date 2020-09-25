using Microsoft.EntityFrameworkCore.Migrations;

namespace WebUniversidade.Migrations
{
    public partial class ColumnDataEntrada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnrollmentDate",
                table: "Estudante",
                newName: "DataEntrada");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataEntrada",
                table: "Estudante",
                newName: "EnrollmentDate");
        }
    }
}
