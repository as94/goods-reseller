using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodsReseller.Infrastructure.Migrations
{
    public partial class MetaToOrderItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "order_items",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateUtc",
                table: "order_items",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "order_items",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "order_items",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDateUtc",
                table: "order_items",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "CreationDateUtc",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "LastUpdateDateUtc",
                table: "order_items");
        }
    }
}
