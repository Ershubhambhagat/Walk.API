using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class _2ndmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walks_WorkDiffucalties_WorkDiffucaltyId",
                table: "walks");

            migrationBuilder.DropColumn(
                name: "WorkDifficultyId",
                table: "walks");

            migrationBuilder.RenameColumn(
                name: "WorkDiffucaltyId",
                table: "walks",
                newName: "WalkDifficultyId");

            migrationBuilder.RenameIndex(
                name: "IX_walks_WorkDiffucaltyId",
                table: "walks",
                newName: "IX_walks_WalkDifficultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_walks_WorkDiffucalties_WalkDifficultyId",
                table: "walks",
                column: "WalkDifficultyId",
                principalTable: "WorkDiffucalties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_walks_WorkDiffucalties_WalkDifficultyId",
                table: "walks");

            migrationBuilder.RenameColumn(
                name: "WalkDifficultyId",
                table: "walks",
                newName: "WorkDiffucaltyId");

            migrationBuilder.RenameIndex(
                name: "IX_walks_WalkDifficultyId",
                table: "walks",
                newName: "IX_walks_WorkDiffucaltyId");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkDifficultyId",
                table: "walks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_walks_WorkDiffucalties_WorkDiffucaltyId",
                table: "walks",
                column: "WorkDiffucaltyId",
                principalTable: "WorkDiffucalties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
