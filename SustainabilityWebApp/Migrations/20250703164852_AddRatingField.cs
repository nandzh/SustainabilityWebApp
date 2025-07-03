using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SustainabilityWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Bad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Brand");
        }
    }
}
