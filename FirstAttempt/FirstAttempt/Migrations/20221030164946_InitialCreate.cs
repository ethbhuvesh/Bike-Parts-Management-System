using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstAttempt.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bikes",
                columns: table => new
                {
                    BikeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BikeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BikeSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BikePrice = table.Column<int>(type: "int", nullable: false),
                    BikeCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.BikeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bikes");
        }
    }
}
