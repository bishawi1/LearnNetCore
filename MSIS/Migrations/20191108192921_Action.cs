using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS.Migrations
{
    public partial class Action : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentTaskStatusId",
                table: "TaskActions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TaskDetailsViewModelId",
                table: "TaskActions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskStatusId",
                table: "TaskActions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Department",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "SQLTaskDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskDate = table.Column<DateTime>(nullable: false),
                    TaskStartDate = table.Column<DateTime>(nullable: false),
                    TaskEndDate = table.Column<DateTime>(nullable: false),
                    TaskOwnerId = table.Column<int>(nullable: false),
                    TaskOwnerName = table.Column<string>(nullable: true),
                    TaskSubject = table.Column<string>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    BranchCode = table.Column<string>(nullable: true),
                    BranchName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectYear = table.Column<string>(nullable: true),
                    ProjectSerial = table.Column<int>(nullable: false),
                    ProjectName = table.Column<string>(nullable: true),
                    TaskStatusId = table.Column<int>(nullable: false),
                    TaskStatusCode = table.Column<string>(nullable: true),
                    StatusName = table.Column<string>(nullable: true),
                    TaskResponsibleId = table.Column<int>(nullable: false),
                    TaskResponsibleName = table.Column<string>(nullable: true),
                    OtherInformation = table.Column<string>(nullable: true),
                    TaskResultDescription = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Time_Stamp = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TaskActionDetails = table.Column<string>(nullable: true),
                    TaskOperation = table.Column<string>(nullable: true),
                    TaskOwnerUserName = table.Column<string>(nullable: true),
                    strGroupBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SQLTaskDetails", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskActions_SQLTaskDetails_TaskDetailsViewModelId",
                table: "TaskActions");

            migrationBuilder.DropTable(
                name: "SQLTaskDetails");

            migrationBuilder.DropIndex(
                name: "IX_TaskActions_TaskDetailsViewModelId",
                table: "TaskActions");

            migrationBuilder.DropColumn(
                name: "CurrentTaskStatusId",
                table: "TaskActions");

            migrationBuilder.DropColumn(
                name: "TaskDetailsViewModelId",
                table: "TaskActions");

            migrationBuilder.DropColumn(
                name: "TaskStatusId",
                table: "TaskActions");

            migrationBuilder.AlterColumn<int>(
                name: "Department",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
