using Microsoft.EntityFrameworkCore.Migrations;

namespace Softuni.Community.Data.Migrations
{
    public partial class updated_GameProblem_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_GameProblems_GameProblemId",
                table: "Choices");

            migrationBuilder.AlterColumn<string>(
                name: "RightAnswer",
                table: "GameProblems",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GameProblemId",
                table: "Choices",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_GameProblems_GameProblemId",
                table: "Choices",
                column: "GameProblemId",
                principalTable: "GameProblems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_GameProblems_GameProblemId",
                table: "Choices");

            migrationBuilder.AlterColumn<int>(
                name: "RightAnswer",
                table: "GameProblems",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "GameProblemId",
                table: "Choices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_GameProblems_GameProblemId",
                table: "Choices",
                column: "GameProblemId",
                principalTable: "GameProblems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
