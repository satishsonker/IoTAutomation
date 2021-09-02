using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class addActivityLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SceneActions_Scenes_SceneId",
                table: "SceneActions");

            migrationBuilder.DropForeignKey(
                name: "FK_Scenes_Devices_DeviceId",
                table: "Scenes");

            migrationBuilder.DropIndex(
                name: "IX_Scenes_DeviceId",
                table: "Scenes");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Scenes");

            migrationBuilder.AlterColumn<int>(
                name: "SceneId",
                table: "SceneActions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ActivityLog",
                columns: table => new
                {
                    ActivityLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLog", x => x.ActivityLogId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SceneActions_DeviceId",
                table: "SceneActions",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SceneActions_Devices_DeviceId",
                table: "SceneActions",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SceneActions_Scenes_SceneId",
                table: "SceneActions",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "SceneId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SceneActions_Devices_DeviceId",
                table: "SceneActions");

            migrationBuilder.DropForeignKey(
                name: "FK_SceneActions_Scenes_SceneId",
                table: "SceneActions");

            migrationBuilder.DropTable(
                name: "ActivityLog");

            migrationBuilder.DropIndex(
                name: "IX_SceneActions_DeviceId",
                table: "SceneActions");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Scenes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SceneId",
                table: "SceneActions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scenes_DeviceId",
                table: "Scenes",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_SceneActions_Scenes_SceneId",
                table: "SceneActions",
                column: "SceneId",
                principalTable: "Scenes",
                principalColumn: "SceneId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Scenes_Devices_DeviceId",
                table: "Scenes",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
