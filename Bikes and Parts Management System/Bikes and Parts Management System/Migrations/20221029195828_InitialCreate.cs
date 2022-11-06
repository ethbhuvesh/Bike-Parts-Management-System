using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bikes_and_Parts_Management_System.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountExpiry",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GraduationMonth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GraduationYear",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Users",
                newName: "UserRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRole",
                table: "Users",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountExpiry",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "GraduationMonth",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GraduationYear",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
