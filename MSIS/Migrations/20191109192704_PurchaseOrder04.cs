using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS.Migrations
{
    public partial class PurchaseOrder04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SQLListPurchaseOrderDetailsViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseOrderYear = table.Column<int>(nullable: false),
                    PurchaseOrderNo = table.Column<int>(nullable: false),
                    PurchaseOrderCode = table.Column<string>(nullable: true),
                    PurchaseOrderDate = table.Column<DateTime>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    SupplierName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyRate = table.Column<float>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Time_Stamp = table.Column<DateTime>(nullable: false),
                    User_Name = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    CurrencyName = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SQLListPurchaseOrderDetailsViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SQLPurchaseOrderDetailsViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseOrderYear = table.Column<int>(nullable: false),
                    PurchaseOrderNo = table.Column<int>(nullable: false),
                    PurchaseOrderCode = table.Column<string>(nullable: true),
                    PurchaseOrderDate = table.Column<DateTime>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    SupplierName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    CurrencyRate = table.Column<float>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    Time_Stamp = table.Column<DateTime>(nullable: false),
                    User_Name = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    CurrencyName = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SQLPurchaseOrderDetailsViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SQLPurchaseOrderItemsViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PuchaseOrderId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    ItemUnitId = table.Column<int>(nullable: false),
                    QNT = table.Column<float>(nullable: false),
                    UnitPrice = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ItemUnitName = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<double>(nullable: false),
                    PurchaseOrderDetailsViewModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SQLPurchaseOrderItemsViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SQLPurchaseOrderItemsViewModel_SQLPurchaseOrderDetailsViewModel_PurchaseOrderDetailsViewModelId",
                        column: x => x.PurchaseOrderDetailsViewModelId,
                        principalTable: "SQLPurchaseOrderDetailsViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SQLPurchaseOrderItemsViewModel_PurchaseOrderDetailsViewModelId",
                table: "SQLPurchaseOrderItemsViewModel",
                column: "PurchaseOrderDetailsViewModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SQLListPurchaseOrderDetailsViewModel");

            migrationBuilder.DropTable(
                name: "SQLPurchaseOrderItemsViewModel");

            migrationBuilder.DropTable(
                name: "SQLPurchaseOrderDetailsViewModel");
        }
    }
}
