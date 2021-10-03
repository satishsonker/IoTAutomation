using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class AddMasterTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CapabilityInterface",
                columns: table => new
                {
                    CapabilityInterfaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapabilityInterfaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapabilityInterface", x => x.CapabilityInterfaceId);
                });

            migrationBuilder.CreateTable(
                name: "CapabilitySupportedProperty",
                columns: table => new
                {
                    CapabilitySupportedPropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapabilitySupportedPropertyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapabilitySupportedProperty", x => x.CapabilitySupportedPropertyId);
                });

            migrationBuilder.CreateTable(
                name: "CapabilityTypes",
                columns: table => new
                {
                    CapabilityTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapabilityTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapabilityTypes", x => x.CapabilityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CapabilityVersion",
                columns: table => new
                {
                    CapabilityVersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapabilityVersionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapabilityVersion", x => x.CapabilityVersionId);
                });

            migrationBuilder.CreateTable(
                name: "DisplayCategory",
                columns: table => new
                {
                    DisplayCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayCategoryValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayCategoryLabel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayCategory", x => x.DisplayCategoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapabilityInterface");

            migrationBuilder.DropTable(
                name: "CapabilitySupportedProperty");

            migrationBuilder.DropTable(
                name: "CapabilityTypes");

            migrationBuilder.DropTable(
                name: "CapabilityVersion");

            migrationBuilder.DropTable(
                name: "DisplayCategory");
        }
    }
}
