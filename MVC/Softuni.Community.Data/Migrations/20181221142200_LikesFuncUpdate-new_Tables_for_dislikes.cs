using Microsoft.EntityFrameworkCore.Migrations;

namespace Softuni.Community.Data.Migrations
{
    public partial class LikesFuncUpdatenew_Tables_for_dislikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserQuestionLikes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserAnswerLikes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestionLikes_UserId",
                table: "UserQuestionLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswerLikes_UserId",
                table: "UserAnswerLikes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswerLikes_AspNetUsers_UserId",
                table: "UserAnswerLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestionLikes_AspNetUsers_UserId",
                table: "UserQuestionLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswerLikes_AspNetUsers_UserId",
                table: "UserAnswerLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestionLikes_AspNetUsers_UserId",
                table: "UserQuestionLikes");

            migrationBuilder.DropIndex(
                name: "IX_UserQuestionLikes_UserId",
                table: "UserQuestionLikes");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswerLikes_UserId",
                table: "UserAnswerLikes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserQuestionLikes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserAnswerLikes");
        }
    }
}
