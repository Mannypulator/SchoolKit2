using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class formteacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherClasses",
                columns: table => new
                {
                    TeacherClassID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClassArmID = table.Column<int>(type: "INTEGER", nullable: false),
                    TeacherID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherClasses", x => x.TeacherClassID);
                    table.ForeignKey(
                        name: "FK_TeacherClasses_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherClasses_ClassArms_ClassArmID",
                        column: x => x.ClassArmID,
                        principalTable: "ClassArms",
                        principalColumn: "ClassArmID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClasses_ClassArmID",
                table: "TeacherClasses",
                column: "ClassArmID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherClasses_TeacherID",
                table: "TeacherClasses",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherClasses");
        }
    }
}
