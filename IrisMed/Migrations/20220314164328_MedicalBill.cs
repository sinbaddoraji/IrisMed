using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrisMed.Migrations
{
    public partial class MedicalBill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalBill",
                table: "IrisUser");

            migrationBuilder.AddColumn<double>(
                name: "MedicalBill",
                table: "Appointments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalBill",
                table: "Appointments");

            migrationBuilder.AddColumn<double>(
                name: "MedicalBill",
                table: "IrisUser",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
