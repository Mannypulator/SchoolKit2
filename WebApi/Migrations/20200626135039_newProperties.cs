using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class newProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Append",
                table: "Schools",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegNoCount",
                table: "Schools",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentCount",
                table: "Schools",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeachersCount",
                table: "Schools",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SSCompulsory",
                columns: table => new
                {
                    SSCompulsoryID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchoolID = table.Column<int>(nullable: false),
                    SubjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSCompulsory", x => x.SSCompulsoryID);
                    table.ForeignKey(
                        name: "FK_SSCompulsory_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SSCompulsory_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SSDrop",
                columns: table => new
                {
                    SSDropID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SchoolID = table.Column<int>(nullable: false),
                    SubjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSDrop", x => x.SSDropID);
                    table.ForeignKey(
                        name: "FK_SSDrop_Schools_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "Schools",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SSDrop_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SSCompulsory_SchoolID",
                table: "SSCompulsory",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SSCompulsory_SubjectID",
                table: "SSCompulsory",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SSDrop_SchoolID",
                table: "SSDrop",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_SSDrop_SubjectID",
                table: "SSDrop",
                column: "SubjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SSCompulsory");

            migrationBuilder.DropTable(
                name: "SSDrop");

            migrationBuilder.DropColumn(
                name: "Append",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "RegNoCount",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "StudentCount",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "TeachersCount",
                table: "Schools");
        }
    }
}
