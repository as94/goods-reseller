using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodsReseller.Infrastructure.Migrations
{
    public partial class AddProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(1024)", nullable: false),
                    UnitPriceValue = table.Column<decimal>(type: "numeric", nullable: true),
                    DiscountPerUnitValue = table.Column<decimal>(type: "numeric", nullable: true),
                    ProductIds = table.Column<string>(type: "json", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreationDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastUpdateDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
