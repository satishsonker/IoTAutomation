using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class UpdateDeviceActionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceactionName",
                table: "DeviceActions",
                newName: "DeciveActionName");

            migrationBuilder.AddColumn<string>(
                name: "DeviceActionValue",
                table: "DeviceActions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceActionValue",
                table: "DeviceActions");

            migrationBuilder.RenameColumn(
                name: "DeciveActionName",
                table: "DeviceActions",
                newName: "DeviceactionName");
        }
    }
}
