using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class UpdatedeviceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomIdentifier",
                table: "Devices",
                maxLength:50,
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirmwareVersion",
                table: "Devices",
                maxLength: 50,
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Devices",
                maxLength: 50,
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Devices",
                maxLength: 50,
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoftwareVersion",
                table: "Devices",
                maxLength: 50,
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomIdentifier",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "FirmwareVersion",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "SoftwareVersion",
                table: "Devices");
        }
    }
}
