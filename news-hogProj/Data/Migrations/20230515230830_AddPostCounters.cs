using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace news_hogProj.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPostCounters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PageViews",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalComments",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageViews",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "TotalComments",
                table: "Posts");
        }
    }
}
