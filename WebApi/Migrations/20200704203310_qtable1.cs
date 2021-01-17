using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class qtable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.RenameTable(
                name: "QAttempts",
                newName: "QuestionAttempts");

            migrationBuilder.RenameIndex(
                name: "IX_QAttempts_TestAttemptID",
                table: "QuestionAttempts",
                newName: "IX_QuestionAttempts_TestAttemptID");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.RenameTable(
                name: "QuestionAttempts",
                newName: "QAttempts");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionAttempts_TestAttemptID",
                table: "QAttempts",
                newName: "IX_QAttempts_TestAttemptID");

           
        }
    }
}
