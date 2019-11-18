using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS.Migrations
{
    public partial class Suppliers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskActions_SQLTaskDetails_TaskDetailsViewModelId",
                table: "TaskActions");

            migrationBuilder.DropIndex(
                name: "IX_TaskActions_TaskDetailsViewModelId",
                table: "TaskActions");

            migrationBuilder.DropColumn(
                name: "TaskDetailsViewModelId",
                table: "TaskActions");

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupplierName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    OtherInformation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.AddColumn<int>(
                name: "TaskDetailsViewModelId",
                table: "TaskActions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskActions_TaskDetailsViewModelId",
                table: "TaskActions",
                column: "TaskDetailsViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskActions_SQLTaskDetails_TaskDetailsViewModelId",
                table: "TaskActions",
                column: "TaskDetailsViewModelId",
                principalTable: "SQLTaskDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
