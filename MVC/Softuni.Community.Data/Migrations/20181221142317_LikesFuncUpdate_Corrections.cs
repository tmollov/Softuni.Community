using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Softuni.Community.Data.Migrations
{
    public partial class LikesFuncUpdate_Corrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswerLikes_AspNetUsers_UserId",
                table: "UserAnswerLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestionLikes_AspNetUsers_UserId",
                table: "UserQuestionLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserQuestionLikes",
                table: "UserQuestionLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswerLikes",
                table: "UserAnswerLikes");

            migrationBuilder.RenameTable(
                name: "UserQuestionLikes",
                newName: "UsersQuestionLikes");

            migrationBuilder.RenameTable(
                name: "UserAnswerLikes",
                newName: "UsersAnswerLikes");

            migrationBuilder.RenameIndex(
                name: "IX_UserQuestionLikes_UserId",
                table: "UsersQuestionLikes",
                newName: "IX_UsersQuestionLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswerLikes_UserId",
                table: "UsersAnswerLikes",
                newName: "IX_UsersAnswerLikes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersQuestionLikes",
                table: "UsersQuestionLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersAnswerLikes",
                table: "UsersAnswerLikes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UsersAnswerDislikes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAnswerDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersAnswerDislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersQuestionDislikes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersQuestionDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersQuestionDislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersAnswerDislikes_UserId",
                table: "UsersAnswerDislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersQuestionDislikes_UserId",
                table: "UsersQuestionDislikes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAnswerLikes_AspNetUsers_UserId",
                table: "UsersAnswerLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersQuestionLikes_AspNetUsers_UserId",
                table: "UsersQuestionLikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersAnswerLikes_AspNetUsers_UserId",
                table: "UsersAnswerLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersQuestionLikes_AspNetUsers_UserId",
                table: "UsersQuestionLikes");

            migrationBuilder.DropTable(
                name: "UsersAnswerDislikes");

            migrationBuilder.DropTable(
                name: "UsersQuestionDislikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersQuestionLikes",
                table: "UsersQuestionLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersAnswerLikes",
                table: "UsersAnswerLikes");

            migrationBuilder.RenameTable(
                name: "UsersQuestionLikes",
                newName: "UserQuestionLikes");

            migrationBuilder.RenameTable(
                name: "UsersAnswerLikes",
                newName: "UserAnswerLikes");

            migrationBuilder.RenameIndex(
                name: "IX_UsersQuestionLikes_UserId",
                table: "UserQuestionLikes",
                newName: "IX_UserQuestionLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersAnswerLikes_UserId",
                table: "UserAnswerLikes",
                newName: "IX_UserAnswerLikes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserQuestionLikes",
                table: "UserQuestionLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswerLikes",
                table: "UserAnswerLikes",
                column: "Id");

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
    }
}
