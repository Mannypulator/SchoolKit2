using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class scorescheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScoreSchemes",
                columns: table => new
                {
                    ScoreShemeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchoolID = table.Column<int>(type: "INTEGER", nullable: false),
                    Test1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Test2 = table.Column<int>(type: "INTEGER", nullable: false),
                    Test3 = table.Column<int>(type: "INTEGER", nullable: false),
                    Exam = table.Column<int>(type: "INTEGER", nullable: false),
                    MinA = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxA = table.Column<int>(type: "INTEGER", nullable: false),
                    MinB = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxB = table.Column<int>(type: "INTEGER", nullable: false),
                    MinC = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxC = table.Column<int>(type: "INTEGER", nullable: false),
                    MinD = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxD = table.Column<int>(type: "INTEGER", nullable: false),
                    MinE = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxE = table.Column<int>(type: "INTEGER", nullable: false),
                    MinP = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxP = table.Column<int>(type: "INTEGER", nullable: false),
                    MinF = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxF = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoreSchemes", x => x.ScoreShemeID);
                    table.ForeignKey(
                        name: "FK_ScoreSchemes_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScoreSchemes_SchoolID",
                table: "ScoreSchemes",
                column: "SchoolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScoreSchemes");
        }
    }
}
