using Microsoft.EntityFrameworkCore.Migrations;

namespace Softuni.Community.Data.Migrations
{
    public partial class DiscussionsModified_QuestionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionsTags");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Tags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_QuestionId",
                table: "Tags",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Questions_QuestionId",
                table: "Tags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Questions_QuestionId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_QuestionId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "QuestionsTags",
                columns: table => new
                {
                    TagId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsTags", x => new { x.TagId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_QuestionsTags_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsTags_QuestionId",
                table: "QuestionsTags",
                column: "QuestionId");
        }
    }
}
