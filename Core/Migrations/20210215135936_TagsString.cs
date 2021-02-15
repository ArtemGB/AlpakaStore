using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class TagsString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Filters",
                newName: "TagsString");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Filters",
                newName: "TagString");
        }
    }
}
