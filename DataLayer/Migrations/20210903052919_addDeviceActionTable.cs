using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class addDeviceActionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SceneActions_Scenes_SceneId",
                table: "SceneActions");

            migrationBuilder.AlterColumn<int>(
                name: "SceneId",
                table: "SceneActions",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceActions",
                columns: table => new
                {
                    DeciveActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false),
                    DeviceactionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceActions", x => x.DeciveActionId);
                    table.ForeignKey(
                        name: "FK_DeviceActions_DeviceType_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceType",
                        principalColumn: "DeviceTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceActions_DeviceTypeId",
                table: "DeviceActions",
                column: "DeviceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SceneActions_Scenes_SceneId",
                table: "SceneActions",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "SceneId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SceneActions_Scenes_SceneId",
                table: "SceneActions");

            migrationBuilder.DropTable(
                name: "DeviceActions");

            migrationBuilder.AlterColumn<int>(
                name: "SceneId",
                table: "SceneActions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SceneActions_Scenes_SceneId",
                table: "SceneActions",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "SceneId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
