using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLakeCore.Infrastructure.EntityFramework.Migrations
{
    public partial class AddCommunityIdConstraintToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_post_community_id",
                table: "post",
                column: "community_id");

            migrationBuilder.AddForeignKey(
                name: "fk_post_community_community_id",
                table: "post",
                column: "community_id",
                principalTable: "community",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_post_community_community_id",
                table: "post");

            migrationBuilder.DropIndex(
                name: "ix_post_community_id",
                table: "post");
        }
    }
}
