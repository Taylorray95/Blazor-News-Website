using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace news_hogProj.Data.Migrations
{
    /// <inheritdoc />
    public partial class CategoryHAshtag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryHashTag",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryHashTag",
                table: "Categories");
        }
    }
}
