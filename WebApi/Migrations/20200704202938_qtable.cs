using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class qtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAttempts");

            migrationBuilder.CreateTable(
                name: "QAttempts",
                columns: table => new
                {
                    QuestionAttemptID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestAttemptID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    OptionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QAttempts", x => x.QuestionAttemptID);
                    table.ForeignKey(
                        name: "FK_QAttempts_Options_OptionID",
                        column: x => x.OptionID,
                        principalTable: "Options",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QAttempts_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QAttempts_TestAttempts_TestAttemptID",
                        column: x => x.TestAttemptID,
                        principalTable: "TestAttempts",
                        principalColumn: "AttemptID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QAttempts_OptionID",
                table: "QAttempts",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_QAttempts_QuestionID",
                table: "QAttempts",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QAttempts_TestAttemptID",
                table: "QAttempts",
                column: "TestAttemptID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QAttempts");

            migrationBuilder.CreateTable(
                name: "QuestionAttempts",
                columns: table => new
                {
                    QuestionAttemptID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_QuestionAttempts_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempts_OptionID",
                table: "QuestionAttempts",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempts_StudentID",
                table: "QuestionAttempts",
                column: "StudentID");
        }
    }
}
