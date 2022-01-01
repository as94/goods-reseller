using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodsReseller.Infrastructure.Migrations
{
    public partial class AddVersions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "users",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "supplies",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "supplies",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "products",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "orders",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "supplies");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "supplies");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "products");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "orders");
        }
    }
}
