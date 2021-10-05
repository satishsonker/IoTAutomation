using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class addStatusInDeviceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Devices",
                type: "nvarchar(max)",
                defaultValueSql:"'Off'",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Devices");
        }
    }
}
