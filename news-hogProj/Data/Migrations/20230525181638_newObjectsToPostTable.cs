using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace news_hogProj.Data.Migrations
{
    /// <inheritdoc />
    public partial class newObjectsToPostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathToImage3",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PathToImage4",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathToImage3",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PathToImage4",
                table: "Posts");
        }
    }
}
