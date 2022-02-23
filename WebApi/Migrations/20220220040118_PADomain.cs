using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class PADomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TeacherComment",
                table: "Results",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PrincipalComment",
                table: "Results",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "AffectiveDomains",
                columns: table => new
                {
                    AffectiveDomainID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Punctuality = table.Column<int>(type: "INTEGER", nullable: false),
                    Attendance = table.Column<int>(type: "INTEGER", nullable: false),
                    Neatness = table.Column<int>(type: "INTEGER", nullable: false),
                    Honesty = table.Column<int>(type: "INTEGER", nullable: false),
                    SelfControl = table.Column<int>(type: "INTEGER", nullable: false),
                    Obedience = table.Column<int>(type: "INTEGER", nullable: false),
                    Activeness = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffectiveDomains", x => x.AffectiveDomainID);
                    table.ForeignKey(
                        name: "FK_AffectiveDomains_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffectiveDomains_Terms_TermID",
                        column: x => x.TermID,
                        principalTable: "Terms",
                        principalColumn: "TermID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychomotorDomains",
                columns: table => new
                {
                    PsychomotorDomainID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Handwriting = table.Column<int>(type: "INTEGER", nullable: false),
                    Fluency = table.Column<int>(type: "INTEGER", nullable: false),
                    Sports = table.Column<int>(type: "INTEGER", nullable: false),
                    DrawingPainting = table.Column<int>(type: "INTEGER", nullable: false),
                    HandlingTools = table.Column<int>(type: "INTEGER", nullable: false),
                    Creativity = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    TermID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychomotorDomains", x => x.PsychomotorDomainID);
                    table.ForeignKey(
                        name: "FK_PsychomotorDomains_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PsychomotorDomains_Terms_TermID",
                        column: x => x.TermID,
                        principalTable: "Terms",
                        principalColumn: "TermID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffectiveDomains_StudentID",
                table: "AffectiveDomains",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_AffectiveDomains_TermID",
                table: "AffectiveDomains",
                column: "TermID");

            migrationBuilder.CreateIndex(
                name: "IX_PsychomotorDomains_StudentID",
                table: "PsychomotorDomains",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_PsychomotorDomains_TermID",
                table: "PsychomotorDomains",
                column: "TermID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffectiveDomains");

            migrationBuilder.DropTable(
                name: "PsychomotorDomains");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherComment",
                table: "Results",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrincipalComment",
                table: "Results",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
