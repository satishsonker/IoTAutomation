using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class DeviceCapability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceCapability",
                columns: table => new
                {
                    DeviceCapabilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false),
                    CapabilityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Varsion = table.Column<string>(type: "nvarchar(max)", nullable: true,defaultValue:"3"),
                    CapabilityInterface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProactivelyReported = table.Column<bool>(type: "bit", nullable: false,defaultValue:true),
                    Retrievable = table.Column<bool>(type: "bit", nullable: false,defaultValue:true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false,defaultValueSql:"getdate()"),
                    Modifieddate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCapability", x => x.DeviceCapabilityId);
                    table.ForeignKey(
                        name: "FK_DeviceCapability_DeviceType_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceType",
                        principalColumn: "DeviceTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCapability_DeviceTypeId",
                table: "DeviceCapability",
                column: "DeviceTypeId");
        }
    }
}
