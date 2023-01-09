using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class _3rdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walks_WorkDiffucalties_WalkDifficultyId",
                table: "walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkDiffucalties",
                table: "WorkDiffucalties");

            migrationBuilder.RenameTable(
                name: "WorkDiffucalties",
                newName: "walkDifficulties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_walkDifficulties",
                table: "walkDifficulties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_walks_walkDifficulties_WalkDifficultyId",
                table: "walks",
                column: "WalkDifficultyId",
                principalTable: "walkDifficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walks_walkDifficulties_WalkDifficultyId",
                table: "walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_walkDifficulties",
                table: "walkDifficulties");

            migrationBuilder.RenameTable(
                name: "walkDifficulties",
                newName: "WorkDiffucalties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkDiffucalties",
                table: "WorkDiffucalties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_walks_WorkDiffucalties_WalkDifficultyId",
                table: "walks",
                column: "WalkDifficultyId",
                principalTable: "WorkDiffucalties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
