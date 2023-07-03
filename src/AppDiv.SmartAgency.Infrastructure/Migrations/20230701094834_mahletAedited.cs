using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class mahletAedited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 1, 12, 48, 33, 862, DateTimeKind.Local).AddTicks(772),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 1, 12, 43, 55, 494, DateTimeKind.Local).AddTicks(5845));

            migrationBuilder.AddColumn<string>(
                name: "Mahlet",
                table: "Applicants",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 48, 33, 866, DateTimeKind.Local).AddTicks(8451));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 48, 33, 866, DateTimeKind.Local).AddTicks(8465));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 48, 33, 866, DateTimeKind.Local).AddTicks(8479));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 48, 33, 866, DateTimeKind.Local).AddTicks(8482));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 48, 33, 866, DateTimeKind.Local).AddTicks(8485));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 48, 33, 866, DateTimeKind.Local).AddTicks(8490));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 48, 33, 866, DateTimeKind.Local).AddTicks(8266));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mahlet",
                table: "Applicants");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 1, 12, 43, 55, 494, DateTimeKind.Local).AddTicks(5845),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 1, 12, 48, 33, 862, DateTimeKind.Local).AddTicks(772));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 43, 55, 499, DateTimeKind.Local).AddTicks(3491));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 43, 55, 499, DateTimeKind.Local).AddTicks(3502));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 43, 55, 499, DateTimeKind.Local).AddTicks(3506));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 43, 55, 499, DateTimeKind.Local).AddTicks(3510));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 43, 55, 499, DateTimeKind.Local).AddTicks(3527));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 43, 55, 499, DateTimeKind.Local).AddTicks(3533));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 1, 12, 43, 55, 499, DateTimeKind.Local).AddTicks(3340));
        }
    }
}
