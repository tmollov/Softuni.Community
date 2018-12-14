using Microsoft.EntityFrameworkCore.Migrations;

namespace Softuni.Community.Data.Migrations
{
    public partial class New_prop_AboutMe_to_UserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "UserInfos",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "UserInfos");
        }
    }
}
