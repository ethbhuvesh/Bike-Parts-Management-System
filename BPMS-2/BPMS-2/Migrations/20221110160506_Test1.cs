using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BPMS_2.Migrations
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetailsModel",
                table: "OrderDetailsModel");

            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "OrderDetailsModel",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetailsModel",
                table: "OrderDetailsModel",
                column: "TableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetailsModel",
                table: "OrderDetailsModel");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "OrderDetailsModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetailsModel",
                table: "OrderDetailsModel",
                column: "ProductId");
        }
    }
}
