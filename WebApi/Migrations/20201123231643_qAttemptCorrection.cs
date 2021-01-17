using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class qAttemptCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAttempts");

            migrationBuilder.DropTable(
                name: "Options");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Correct = table.Column<bool>(type: "INTEGER", nullable: false),
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
                    TestAttemptID = table.Column<int>(type: "INTEGER", nullable: false)
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
                        name: "FK_QuestionAttempts_TestAttempts_TestAttemptID",
                        column: x => x.TestAttemptID,
                        principalTable: "TestAttempts",
                        principalColumn: "AttemptID",
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
                name: "IX_QuestionAttempts_TestAttemptID",
                table: "QuestionAttempts",
                column: "TestAttemptID");
        }
    }
}
