using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class CheckModifications202307030500 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 3, 17, 2, 22, 34, DateTimeKind.Local).AddTicks(7493),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 2, 20, 20, 32, 713, DateTimeKind.Local).AddTicks(8960));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 3, 17, 2, 22, 49, DateTimeKind.Local).AddTicks(5985));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 3, 17, 2, 22, 49, DateTimeKind.Local).AddTicks(6004));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 3, 17, 2, 22, 49, DateTimeKind.Local).AddTicks(6013));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 3, 17, 2, 22, 49, DateTimeKind.Local).AddTicks(6021));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 3, 17, 2, 22, 49, DateTimeKind.Local).AddTicks(6029));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 3, 17, 2, 22, 49, DateTimeKind.Local).AddTicks(6038));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 3, 17, 2, 22, 49, DateTimeKind.Local).AddTicks(5710));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisteredDate",
                table: "TicketRegistrations",
                newName: "RegiteredDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 2, 20, 20, 32, 713, DateTimeKind.Local).AddTicks(8960),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 3, 17, 2, 22, 34, DateTimeKind.Local).AddTicks(7493));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(230));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(255));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(270));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(276));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 718, DateTimeKind.Local).AddTicks(9994));
        }
    }
}
