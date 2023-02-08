using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class LastInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walk_Regions_RegionId",
                table: "Walk");

            migrationBuilder.DropForeignKey(
                name: "FK_Walk_WalkDifficulty_WalkDifficultyId",
                table: "Walk");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Walk",
                table: "Walk");

            migrationBuilder.RenameTable(
                name: "Walk",
                newName: "Walks");

            migrationBuilder.RenameIndex(
                name: "IX_Walk_WalkDifficultyId",
                table: "Walks",
                newName: "IX_Walks_WalkDifficultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Walk_RegionId",
                table: "Walks",
                newName: "IX_Walks_RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Walks",
                table: "Walks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_WalkDifficulty_WalkDifficultyId",
                table: "Walks",
                column: "WalkDifficultyId",
                principalTable: "WalkDifficulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks");

            migrationBuilder.DropForeignKey(
                name: "FK_Walks_WalkDifficulty_WalkDifficultyId",
                table: "Walks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Walks",
                table: "Walks");

            migrationBuilder.RenameTable(
                name: "Walks",
                newName: "Walk");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_WalkDifficultyId",
                table: "Walk",
                newName: "IX_Walk_WalkDifficultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_RegionId",
                table: "Walk",
                newName: "IX_Walk_RegionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Walk",
                table: "Walk",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Walk_Regions_RegionId",
                table: "Walk",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Walk_WalkDifficulty_WalkDifficultyId",
                table: "Walk",
                column: "WalkDifficultyId",
                principalTable: "WalkDifficulty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
