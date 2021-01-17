using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class codep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<string>(
                name: "AdminID",
                table: "Schools",
                type: "TEXT",
                nullable: true,
                defaultValue: "");



            migrationBuilder.CreateIndex(
                name: "IX_Schools_AdminID",
                table: "Schools",
                column: "AdminID");

                  }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropIndex(
                name: "IX_Schools_AdminID",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Schools");

                   }
    }
}
