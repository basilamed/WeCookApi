using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeCook.Data.Migrations
{
    /// <inheritdoc />
    public partial class taste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Taste",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taste",
                table: "Recipes");
        }
    }
}
