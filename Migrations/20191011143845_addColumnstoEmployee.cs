using Microsoft.EntityFrameworkCore.Migrations;

namespace MSIS.Migrations
{
    public partial class addColumnstoEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationDegree",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityNo",
                table: "Employees",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MobileNo",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherInformation",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkMobileNo",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EducationDegree",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IdentityNo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MobileNo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "OtherInformation",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "WorkMobileNo",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
