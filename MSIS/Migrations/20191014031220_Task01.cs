using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS.Migrations
{
    public partial class Task01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskDate = table.Column<DateTime>(nullable: false),
                    TaskStartDate = table.Column<DateTime>(nullable: false),
                    TaskEndDate = table.Column<DateTime>(nullable: false),
                    TaskOwnerId = table.Column<int>(nullable: false),
                    TaskSubject = table.Column<string>(type: "nvarchar(300)", nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    TaskStatusId = table.Column<int>(nullable: false),
                    TaskResponsibleId = table.Column<int>(nullable: false),
                    OtherInformation = table.Column<string>(type: "nvarchar(Max)", nullable: true),
                    TaskResultDescription = table.Column<string>(type: "nvarchar(Max)", nullable: true),
                    StartDate = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Time_Stamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
