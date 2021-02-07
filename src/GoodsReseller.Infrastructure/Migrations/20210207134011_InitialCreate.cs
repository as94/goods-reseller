using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoodsReseller.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    PasswordHash_Value = table.Column<string>(type: "varchar(1024)", nullable: true),
                    Role_Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    Role_Id = table.Column<int>(type: "integer", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreationDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastUpdateDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
