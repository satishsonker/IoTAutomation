using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class AddUserPermistionTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermission_Users_UserPermissionId",
                table: "UserPermission");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_Users_UserPermissionId",
                table: "UserPermission",
                column: "UserPermissionId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermission_Users_UserPermissionId",
                table: "UserPermission");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_Users_UserPermissionId",
                table: "UserPermission",
                column: "UserPermissionId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
