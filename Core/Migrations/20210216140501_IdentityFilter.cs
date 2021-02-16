using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class IdentityFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Filters_FilterId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FilterId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FilterId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilterId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_FilterId",
                table: "Products",
                column: "FilterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Filters_FilterId",
                table: "Products",
                column: "FilterId",
                principalTable: "Filters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
