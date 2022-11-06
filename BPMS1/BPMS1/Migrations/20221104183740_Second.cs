using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMS1.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BikesProductId",
                table: "OrderDetailsModel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PartsProductId",
                table: "OrderDetailsModel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailsModel_BikesProductId",
                table: "OrderDetailsModel",
                column: "BikesProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailsModel_PartsProductId",
                table: "OrderDetailsModel",
                column: "PartsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailsModel_BikesModel_BikesProductId",
                table: "OrderDetailsModel",
                column: "BikesProductId",
                principalTable: "BikesModel",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailsModel_PartsModel_PartsProductId",
                table: "OrderDetailsModel",
                column: "PartsProductId",
                principalTable: "PartsModel",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailsModel_BikesModel_BikesProductId",
                table: "OrderDetailsModel");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailsModel_PartsModel_PartsProductId",
                table: "OrderDetailsModel");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailsModel_BikesProductId",
                table: "OrderDetailsModel");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailsModel_PartsProductId",
                table: "OrderDetailsModel");

            migrationBuilder.DropColumn(
                name: "BikesProductId",
                table: "OrderDetailsModel");

            migrationBuilder.DropColumn(
                name: "PartsProductId",
                table: "OrderDetailsModel");
        }
    }
}
