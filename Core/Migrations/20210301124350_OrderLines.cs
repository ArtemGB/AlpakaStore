using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class OrderLines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Completed Order Lines_OrderLines_Id",
                table: "Completed Order Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Completed Orders_Orders_Id",
                table: "Completed Orders");

            migrationBuilder.RenameColumn(
                name: "CompleteDate",
                table: "Completed Orders",
                newName: "CreateDateTime");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "OrderLines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Completed Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Completed Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompleteDateTime",
                table: "Completed Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeliveryType",
                table: "Completed Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Completed Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Completed Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Completed Order Lines",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Completed Order Lines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Completed Order Lines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Completed Order Lines",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Completed Order Lines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Completed Orders");

            migrationBuilder.DropColumn(
                name: "CompleteDateTime",
                table: "Completed Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "Completed Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Completed Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Completed Orders");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Completed Order Lines");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Completed Order Lines");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Completed Order Lines");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Completed Order Lines");

            migrationBuilder.RenameColumn(
                name: "CreateDateTime",
                table: "Completed Orders",
                newName: "CompleteDate");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Completed Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Completed Order Lines",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Completed Order Lines_OrderLines_Id",
                table: "Completed Order Lines",
                column: "Id",
                principalTable: "OrderLines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Completed Orders_Orders_Id",
                table: "Completed Orders",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
