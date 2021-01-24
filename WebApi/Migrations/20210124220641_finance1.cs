using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class finance1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentFee_AspNetUsers_StudentID",
                table: "StudentFee");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentFee_Fees_FeeID",
                table: "StudentFee");

            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentFee",
                table: "StudentFee");

            migrationBuilder.RenameTable(
                name: "StudentFee",
                newName: "StudentFees");

            migrationBuilder.RenameIndex(
                name: "IX_StudentFee_StudentID",
                table: "StudentFees",
                newName: "IX_StudentFees_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentFee_FeeID",
                table: "StudentFees",
                newName: "IX_StudentFees_FeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentFees",
                table: "StudentFees",
                column: "StudentFeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFees_AspNetUsers_StudentID",
                table: "StudentFees",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFees_Fees_FeeID",
                table: "StudentFees",
                column: "FeeID",
                principalTable: "Fees",
                principalColumn: "FeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentFees_AspNetUsers_StudentID",
                table: "StudentFees");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentFees_Fees_FeeID",
                table: "StudentFees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentFees",
                table: "StudentFees");

            migrationBuilder.RenameTable(
                name: "StudentFees",
                newName: "StudentFee");

            migrationBuilder.RenameIndex(
                name: "IX_StudentFees_StudentID",
                table: "StudentFee",
                newName: "IX_StudentFee_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentFees_FeeID",
                table: "StudentFee",
                newName: "IX_StudentFee_FeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentFee",
                table: "StudentFee",
                column: "StudentFeeID");

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    ProductSalesID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discount = table.Column<decimal>(type: "TEXT", nullable: true),
                    ProductID = table.Column<long>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    SalePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    StudentID = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => x.ProductSalesID);
                    table.ForeignKey(
                        name: "FK_ProductSales_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSales_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductID",
                table: "ProductSales",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_StudentID",
                table: "ProductSales",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFee_AspNetUsers_StudentID",
                table: "StudentFee",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentFee_Fees_FeeID",
                table: "StudentFee",
                column: "FeeID",
                principalTable: "Fees",
                principalColumn: "FeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
