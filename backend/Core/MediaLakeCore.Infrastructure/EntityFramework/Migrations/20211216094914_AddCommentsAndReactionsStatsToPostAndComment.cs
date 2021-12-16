using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLakeCore.Infrastructure.EntityFramework.Migrations
{
    public partial class AddCommentsAndReactionsStatsToPostAndComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "comments_count",
                table: "post",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "dislikes_count",
                table: "post",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "likes_count",
                table: "post",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "dislikes_count",
                table: "comment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "likes_count",
                table: "comment",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comments_count",
                table: "post");

            migrationBuilder.DropColumn(
                name: "dislikes_count",
                table: "post");

            migrationBuilder.DropColumn(
                name: "likes_count",
                table: "post");

            migrationBuilder.DropColumn(
                name: "dislikes_count",
                table: "comment");

            migrationBuilder.DropColumn(
                name: "likes_count",
                table: "comment");
        }
    }
}
