using Microsoft.EntityFrameworkCore.Migrations;

namespace Softuni.Community.Data.Migrations
{
    public partial class Added_Jokes_Memes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joke_AspNetUsers_PublisherId",
                table: "Joke");

            migrationBuilder.DropForeignKey(
                name: "FK_Meme_AspNetUsers_PublisherId",
                table: "Meme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meme",
                table: "Meme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Joke",
                table: "Joke");

            migrationBuilder.RenameTable(
                name: "Meme",
                newName: "Memes");

            migrationBuilder.RenameTable(
                name: "Joke",
                newName: "Jokes");

            migrationBuilder.RenameIndex(
                name: "IX_Meme_PublisherId",
                table: "Memes",
                newName: "IX_Memes_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Joke_PublisherId",
                table: "Jokes",
                newName: "IX_Jokes_PublisherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memes",
                table: "Memes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jokes",
                table: "Jokes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jokes_AspNetUsers_PublisherId",
                table: "Jokes",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_AspNetUsers_PublisherId",
                table: "Memes",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jokes_AspNetUsers_PublisherId",
                table: "Jokes");

            migrationBuilder.DropForeignKey(
                name: "FK_Memes_AspNetUsers_PublisherId",
                table: "Memes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memes",
                table: "Memes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jokes",
                table: "Jokes");

            migrationBuilder.RenameTable(
                name: "Memes",
                newName: "Meme");

            migrationBuilder.RenameTable(
                name: "Jokes",
                newName: "Joke");

            migrationBuilder.RenameIndex(
                name: "IX_Memes_PublisherId",
                table: "Meme",
                newName: "IX_Meme_PublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Jokes_PublisherId",
                table: "Joke",
                newName: "IX_Joke_PublisherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meme",
                table: "Meme",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Joke",
                table: "Joke",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Joke_AspNetUsers_PublisherId",
                table: "Joke",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meme_AspNetUsers_PublisherId",
                table: "Meme",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
