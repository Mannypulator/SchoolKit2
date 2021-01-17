using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class termAdjustments2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    TermID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<int>(nullable: false),
                    Session = table.Column<string>(nullable: true),
                    Current = table.Column<bool>(nullable: false),
                    SchoolID = table.Column<int>(nullable: false),
                    SessionStart = table.Column<DateTime>(nullable: false),
                    SessionEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.TermID);
                    table.ForeignKey(
                        name: "FK_Terms_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });


            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "Terms");
        }
    }
}
