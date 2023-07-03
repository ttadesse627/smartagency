using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class ConfiguartionEditedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 30, 17, 33, 0, 949, DateTimeKind.Local).AddTicks(6159),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 30, 16, 50, 40, 728, DateTimeKind.Local).AddTicks(6353));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 17, 33, 0, 954, DateTimeKind.Local).AddTicks(937));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 17, 33, 0, 954, DateTimeKind.Local).AddTicks(947));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 17, 33, 0, 954, DateTimeKind.Local).AddTicks(951));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 17, 33, 0, 954, DateTimeKind.Local).AddTicks(954));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 17, 33, 0, 954, DateTimeKind.Local).AddTicks(959));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 17, 33, 0, 954, DateTimeKind.Local).AddTicks(975));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 17, 33, 0, 954, DateTimeKind.Local).AddTicks(769));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 30, 16, 50, 40, 728, DateTimeKind.Local).AddTicks(6353),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 30, 17, 33, 0, 949, DateTimeKind.Local).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 16, 50, 40, 734, DateTimeKind.Local).AddTicks(4971));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 16, 50, 40, 734, DateTimeKind.Local).AddTicks(4988));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 16, 50, 40, 734, DateTimeKind.Local).AddTicks(4994));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 16, 50, 40, 734, DateTimeKind.Local).AddTicks(5016));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 16, 50, 40, 734, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 16, 50, 40, 734, DateTimeKind.Local).AddTicks(5029));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 30, 16, 50, 40, 734, DateTimeKind.Local).AddTicks(4708));
        }
    }
}
