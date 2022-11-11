using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMS_2.Migrations
{
    public partial class Test6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RentalBikeImage",
                table: "RentBikesModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BikePartImage",
                table: "ProductsModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalBikeImage",
                table: "RentBikesModel");

            migrationBuilder.DropColumn(
                name: "BikePartImage",
                table: "ProductsModel");
        }
    }
}
