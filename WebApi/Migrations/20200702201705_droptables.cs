using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class droptables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAttempts");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Questions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Anwser = table.Column<int>(type: "INTEGER", nullable: false),
                    Qn = table.Column<string>(type: "TEXT", nullable: false),
                    TestID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Opt = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionID);
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAttempts",
                columns: table => new
                {
                    QuestionAttemptID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestionID = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAttempts", x => x.QuestionAttemptID);
                    table.ForeignKey(
                        name: "FK_QuestionAttempts_Options_OptionID",
                        column: x => x.OptionID,
                        principalTable: "Options",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAttempts_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAttempts_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionID",
                table: "Options",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempts_OptionID",
                table: "QuestionAttempts",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempts_QuestionID",
                table: "QuestionAttempts",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempts_StudentID",
                table: "QuestionAttempts",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestID",
                table: "Questions",
                column: "TestID");
        }
    }
}
