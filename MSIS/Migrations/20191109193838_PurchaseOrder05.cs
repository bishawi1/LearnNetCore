using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS.Migrations
{
    public partial class PurchaseOrder05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SQLPurchaseOrderItemsViewModel_SQLPurchaseOrderDetailsViewModel_PurchaseOrderDetailsViewModelId",
                table: "SQLPurchaseOrderItemsViewModel");

            migrationBuilder.DropIndex(
                name: "IX_SQLPurchaseOrderItemsViewModel_PurchaseOrderDetailsViewModelId",
                table: "SQLPurchaseOrderItemsViewModel");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderDetailsViewModelId",
                table: "SQLPurchaseOrderItemsViewModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderDetailsViewModelId",
                table: "SQLPurchaseOrderItemsViewModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SQLPurchaseOrderItemsViewModel_PurchaseOrderDetailsViewModelId",
                table: "SQLPurchaseOrderItemsViewModel",
                column: "PurchaseOrderDetailsViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SQLPurchaseOrderItemsViewModel_SQLPurchaseOrderDetailsViewModel_PurchaseOrderDetailsViewModelId",
                table: "SQLPurchaseOrderItemsViewModel",
                column: "PurchaseOrderDetailsViewModelId",
                principalTable: "SQLPurchaseOrderDetailsViewModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
