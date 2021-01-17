using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class co : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllOptionsCorrect",
                table: "Questions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CorrectOptions",
                table: "Questions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllOptionsCorrect",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CorrectOptions",
                table: "Questions");
        }
    }
}
