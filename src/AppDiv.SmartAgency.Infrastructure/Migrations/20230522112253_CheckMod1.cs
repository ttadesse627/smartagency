using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class CheckMod1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPaidAmount",
                table: "Payments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 22, 14, 22, 52, 599, DateTimeKind.Local).AddTicks(3373),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 22, 11, 28, 1, 295, DateTimeKind.Local).AddTicks(8578));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 22, 11, 28, 1, 295, DateTimeKind.Local).AddTicks(8578),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 22, 14, 22, 52, 599, DateTimeKind.Local).AddTicks(3373));

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPaidAmount",
                table: "Payments",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
