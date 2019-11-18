using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS.Migrations
{
    public partial class PurchaseOrder01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseOrderYear = table.Column<int>(nullable: false),
                    PurchaseOrderNo = table.Column<int>(nullable: false),
                    PurchaseOrderCode = table.Column<string>(nullable: false),
                    PurchaseOrderDate = table.Column<DateTime>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyRate = table.Column<float>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Time_Stamp = table.Column<DateTime>(nullable: false),
                    User_Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrdersDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PuchaseOrderId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(nullable: false),
                    QNT = table.Column<float>(nullable: false),
                    UnitPrice = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrdersDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "PurchaseOrdersDetails");
        }
    }
}
