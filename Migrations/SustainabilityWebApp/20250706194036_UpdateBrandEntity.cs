using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SustainabilityWebApp.Migrations.SustainabilityWebApp
{
    /// <inheritdoc />
    public partial class UpdateBrandEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoundedDate",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "Brand");

            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<double>(
                name: "AnimalCrueltyRate",
                table: "Brand",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "CountryOfOrigin",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PeopleRate",
                table: "Brand",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PlanetSustainabilityRate",
                table: "Brand",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PricingRate",
                table: "Brand",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Rated",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SmileyFace",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimalCrueltyRate",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "CountryOfOrigin",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "PeopleRate",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "PlanetSustainabilityRate",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "PricingRate",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "Rated",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "SmileyFace",
                table: "Brand");

            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "FoundedDate",
                table: "Brand",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<float>(
                name: "Score",
                table: "Brand",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
