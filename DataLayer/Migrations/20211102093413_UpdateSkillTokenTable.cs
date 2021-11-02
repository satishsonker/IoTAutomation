using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.DataLayer.Migrations
{
    public partial class UpdateSkillTokenTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "SkillTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "SkillTokens",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "SkillTokens");

            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "SkillTokens");
        }
    }
}
