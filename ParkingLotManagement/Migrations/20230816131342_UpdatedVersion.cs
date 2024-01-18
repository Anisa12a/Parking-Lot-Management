using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLotManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PricingPlans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ParkingSpots",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PricingPlans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ParkingSpots");
        }
    }
}
