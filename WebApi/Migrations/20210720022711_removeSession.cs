using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class removeSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionStart",
                table: "Schools");

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionStart",
                table: "Sessions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionStart",
                table: "Sessions");

            migrationBuilder.AddColumn<bool>(
                name: "SessionStart",
                table: "Schools",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
