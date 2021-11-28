using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLakeCore.Infrastructure.EntityFramework.Migrations
{
    public partial class AddRelationsToReactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_post_reaction_post_id",
                table: "post_reaction",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "ix_comment_reaction_comment_id",
                table: "comment_reaction",
                column: "comment_id");

            migrationBuilder.AddForeignKey(
                name: "fk_comment_reaction_comment_comment_id",
                table: "comment_reaction",
                column: "comment_id",
                principalTable: "comment",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_post_reaction_post_post_id",
                table: "post_reaction",
                column: "post_id",
                principalTable: "post",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comment_reaction_comment_comment_id",
                table: "comment_reaction");

            migrationBuilder.DropForeignKey(
                name: "fk_post_reaction_post_post_id",
                table: "post_reaction");

            migrationBuilder.DropIndex(
                name: "ix_post_reaction_post_id",
                table: "post_reaction");

            migrationBuilder.DropIndex(
                name: "ix_comment_reaction_comment_id",
                table: "comment_reaction");
        }
    }
}
