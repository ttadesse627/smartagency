using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class CheckMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repersentative_Addresses_AddressId",
                table: "Repersentative");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 22, 11, 28, 1, 295, DateTimeKind.Local).AddTicks(8578),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 21, 12, 12, 6, 236, DateTimeKind.Local).AddTicks(7799));

            migrationBuilder.AddForeignKey(
                name: "FK_Repersentative_Addresses_AddressId",
                table: "Repersentative",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repersentative_Addresses_AddressId",
                table: "Repersentative");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 21, 12, 12, 6, 236, DateTimeKind.Local).AddTicks(7799),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 22, 11, 28, 1, 295, DateTimeKind.Local).AddTicks(8578));

            migrationBuilder.AddForeignKey(
                name: "FK_Repersentative_Addresses_AddressId",
                table: "Repersentative",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
