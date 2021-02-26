using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodsReseller.Infrastructure.Migrations
{
    public partial class AddSuppliesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "supplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierInfo = table.Column<string>(type: "json", nullable: true),
                    TotalCostValue = table.Column<decimal>(type: "numeric", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreationDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastUpdateDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "supply_items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnitPriceValue = table.Column<decimal>(type: "numeric", nullable: true),
                    QuantityValue = table.Column<int>(type: "integer", nullable: true),
                    DiscountPerUnitValue = table.Column<decimal>(type: "numeric", nullable: true),
                    SupplyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreationDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastUpdateDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supply_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_supply_items_supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "supplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_supply_items_SupplyId",
                table: "supply_items",
                column: "SupplyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "supply_items");

            migrationBuilder.DropTable(
                name: "supplies");
        }
    }
}
