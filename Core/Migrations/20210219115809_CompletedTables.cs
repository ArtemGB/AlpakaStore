using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class CompletedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Completed Order Lines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Completed Order Lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Completed Order Lines_OrderLines_Id",
                        column: x => x.Id,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Completed Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Completed Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Completed Orders_Orders_Id",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Completed Order Lines");

            migrationBuilder.DropTable(
                name: "Completed Orders");
        }
    }
}
