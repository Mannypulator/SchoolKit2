using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class fixResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Terms_TermID",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_TermID",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "TermID",
                table: "Results");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TermID",
                table: "Results",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Results_TermID",
                table: "Results",
                column: "TermID");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Terms_TermID",
                table: "Results",
                column: "TermID",
                principalTable: "Terms",
                principalColumn: "TermID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
