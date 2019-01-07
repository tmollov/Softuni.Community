using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Softuni.Community.Data.Migrations
{
    public partial class deleted_Memes_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Memes");

            migrationBuilder.RenameColumn(
                name: "IsProfileSettedUp",
                table: "AspNetUsers",
                newName: "IsProfileSetUp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsProfileSettedUp",
                table: "AspNetUsers",
                newName: "IsProfileSetUp");

            migrationBuilder.CreateTable(
                name: "Memes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dislikes = table.Column<int>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: false),
                    PublisherId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memes_AspNetUsers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Memes_PublisherId",
                table: "Memes",
                column: "PublisherId");
        }
    }
}
