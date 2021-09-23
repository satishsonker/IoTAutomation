using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class CapabilityUpdateVersionRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Varsion",
                table: "DeviceCapability");

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "DeviceCapability",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "DeviceCapability");

            migrationBuilder.AddColumn<string>(
                name: "Varsion",
                table: "DeviceCapability",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
