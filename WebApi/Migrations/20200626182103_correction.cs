using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SSDrop",
                newName: "SSDrops");

            migrationBuilder.RenameTable(
                name: "SSCompulsory",
                newName: "SSCompulsories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SSDrops",
                newName: "SSDrop");

            migrationBuilder.RenameTable(
                name: "SSCompulsories",
                newName: "SSCompulsory");

           
        }
    }
}
