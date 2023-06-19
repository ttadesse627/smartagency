using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class AuthorizedPersonColumnEditedMigrtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manager",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "ViseManager",
                table: "CompanySettings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 14, 16, 28, 26, 718, DateTimeKind.Local).AddTicks(9487),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 14, 16, 19, 45, 750, DateTimeKind.Local).AddTicks(8879));

            migrationBuilder.AddColumn<string>(
                name: "AuthorizedPerson",
                table: "CompanySettings",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizedPerson",
                table: "CompanySettings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 14, 16, 19, 45, 750, DateTimeKind.Local).AddTicks(8879),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 14, 16, 28, 26, 718, DateTimeKind.Local).AddTicks(9487));

            migrationBuilder.AddColumn<bool>(
                name: "Manager",
                table: "CompanySettings",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ViseManager",
                table: "CompanySettings",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
