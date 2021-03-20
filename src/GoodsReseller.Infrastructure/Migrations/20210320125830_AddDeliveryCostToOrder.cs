using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodsReseller.Infrastructure.Migrations
{
    public partial class AddDeliveryCostToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryCostValue",
                table: "orders",
                type: "numeric",
                nullable: true,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryCostValue",
                table: "orders");
        }
    }
}
