using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class annualenrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "grade",
                table: "Enrollments",
                newName: "Grade");

            migrationBuilder.CreateTable(
                name: "AnnualEnrollments",
                columns: table => new
                {
                    AnnualEnrollmentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    ClassSubjectID = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstTerm = table.Column<int>(type: "INTEGER", nullable: false),
                    SecondTerm = table.Column<int>(type: "INTEGER", nullable: false),
                    ThirdTerm = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false),
                    grade = table.Column<int>(type: "INTEGER", nullable: false),
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualEnrollments", x => x.AnnualEnrollmentID);
                    table.ForeignKey(
                        name: "FK_AnnualEnrollments_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnualEnrollments_ClassSubjects_ClassSubjectID",
                        column: x => x.ClassSubjectID,
                        principalTable: "ClassSubjects",
                        principalColumn: "ClassSubjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnnualEnrollments_Terms_TermID",
                        column: x => x.TermID,
                        principalTable: "Terms",
                        principalColumn: "TermID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnnualEnrollments_ClassSubjectID",
                table: "AnnualEnrollments",
                column: "ClassSubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualEnrollments_StudentID",
                table: "AnnualEnrollments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualEnrollments_TermID",
                table: "AnnualEnrollments",
                column: "TermID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnualEnrollments");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Enrollments",
                newName: "grade");
        }
    }
}
