using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class grade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "grade",
                table: "AnnualEnrollments",
                newName: "Grade");

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "AnnualEnrollments",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "AnnualEnrollments",
                newName: "grade");

            migrationBuilder.AlterColumn<int>(
                name: "Total",
                table: "AnnualEnrollments",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
