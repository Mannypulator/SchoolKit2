using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class qoptions1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionAttempts",
                columns: table => new
                {
                    QuestionAttemptID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TestAttemptID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    OptSeed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAttempts", x => x.QuestionAttemptID);
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

            migrationBuilder.CreateTable(
                name: "QAttemptsOptions",
                columns: table => new
                {
                    QAttemptsOptionID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionAttemptID = table.Column<int>(nullable: false),
                    OptionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QAttemptsOptions", x => x.QAttemptsOptionID);
                    table.ForeignKey(
                        name: "FK_QAttemptsOptions_Options_OptionID",
                        column: x => x.OptionID,
                        principalTable: "Options",
                        principalColumn: "OptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QAttemptsOptions_QuestionAttempts_QuestionAttemptID",
                        column: x => x.QuestionAttemptID,
                        principalTable: "QuestionAttempts",
                        principalColumn: "QuestionAttemptID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QAttemptsOptions_OptionID",
                table: "QAttemptsOptions",
                column: "OptionID");

            migrationBuilder.CreateIndex(
                name: "IX_QAttemptsOptions_QuestionAttemptID",
                table: "QAttemptsOptions",
                column: "QuestionAttemptID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempts_QuestionID",
                table: "QuestionAttempts",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAttempts_TestAttemptID",
                table: "QuestionAttempts",
                column: "TestAttemptID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QAttemptsOptions");

            migrationBuilder.DropTable(
                name: "QuestionAttempts");
        }
    }
}
