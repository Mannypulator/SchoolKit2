using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class lgaRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LGAs_LgaID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LgaID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LgaID",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LgaID",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LgaID",
                table: "AspNetUsers",
                column: "LgaID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LGAs_LgaID",
                table: "AspNetUsers",
                column: "LgaID",
                principalTable: "LGAs",
                principalColumn: "LgaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
