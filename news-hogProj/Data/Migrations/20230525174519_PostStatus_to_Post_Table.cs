using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace news_hogProj.Data.Migrations
{
    /// <inheritdoc />
    public partial class PostStatus_to_Post_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Posts");
        }
    }
}
