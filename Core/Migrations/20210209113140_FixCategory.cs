using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class FixCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "SubCategoriesJSON",
                table: "Categories",
                newName: "SubCategoriesJson");

            migrationBuilder.RenameColumn(
                name: "ParrentCategoryId",
                table: "Categories",
                newName: "ParentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParrentCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "SubCategoriesJson",
                table: "Categories",
                newName: "SubCategoriesJSON");

            migrationBuilder.RenameColumn(
                name: "ParentCategoryId",
                table: "Categories",
                newName: "ParrentCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                newName: "IX_Categories_ParrentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParrentCategoryId",
                table: "Categories",
                column: "ParrentCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
