using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaLakeCore.Infrastructure.EntityFramework.Migrations
{
    public partial class AddCommentReactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "comment_reaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    is_like = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_reaction", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment_reaction");
        }
    }
}
