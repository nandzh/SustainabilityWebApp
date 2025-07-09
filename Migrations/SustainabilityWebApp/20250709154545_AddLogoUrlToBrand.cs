using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SustainabilityWebApp.Migrations.SustainabilityWebApp
{
    /// <inheritdoc />
    public partial class AddLogoUrlToBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Brand");
        }
    }
}
