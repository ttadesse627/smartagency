using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterPermissionActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 10, 11, 16, 56, 974, DateTimeKind.Local).AddTicks(4284),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 9, 17, 14, 3, 935, DateTimeKind.Local).AddTicks(2203));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("062bf23f-7926-4398-8cd9-c29bfd9ef851"),
                column: "Actions",
                value: "[2,4,0]");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("062bf23f-7926-4398-8cd9-c29bfd9ef852"),
                column: "Actions",
                value: "[2,1,4]");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("062bf23f-7926-4398-8cd9-c29bfd9ef853"),
                column: "Actions",
                value: "[3,4,2,5]");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("062bf23f-7926-4398-8cd9-c29bfd9ef854"),
                column: "Actions",
                value: "[4,1,0,2]");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 10, 11, 16, 56, 984, DateTimeKind.Local).AddTicks(3590));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 10, 11, 16, 56, 984, DateTimeKind.Local).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 10, 11, 16, 56, 984, DateTimeKind.Local).AddTicks(3649));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 10, 11, 16, 56, 984, DateTimeKind.Local).AddTicks(3656));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 10, 11, 16, 56, 984, DateTimeKind.Local).AddTicks(3661));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 10, 11, 16, 56, 984, DateTimeKind.Local).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("6b912c00-9df3-47a1-a524-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 10, 11, 16, 56, 984, DateTimeKind.Local).AddTicks(3676));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 10, 11, 16, 56, 984, DateTimeKind.Local).AddTicks(3120));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 9, 17, 14, 3, 935, DateTimeKind.Local).AddTicks(2203),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 10, 11, 16, 56, 974, DateTimeKind.Local).AddTicks(4284));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("062bf23f-7926-4398-8cd9-c29bfd9ef851"),
                column: "Actions",
                value: "[4,1,6,2]");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("062bf23f-7926-4398-8cd9-c29bfd9ef852"),
                column: "Actions",
                value: "[3,6,4,2]");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("062bf23f-7926-4398-8cd9-c29bfd9ef853"),
                column: "Actions",
                value: "[2,1]");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("062bf23f-7926-4398-8cd9-c29bfd9ef854"),
                column: "Actions",
                value: "[2,1,6,3]");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 9, 17, 14, 3, 950, DateTimeKind.Local).AddTicks(262));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 9, 17, 14, 3, 950, DateTimeKind.Local).AddTicks(292));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 9, 17, 14, 3, 950, DateTimeKind.Local).AddTicks(303));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 9, 17, 14, 3, 950, DateTimeKind.Local).AddTicks(310));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 9, 17, 14, 3, 950, DateTimeKind.Local).AddTicks(335));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 9, 17, 14, 3, 950, DateTimeKind.Local).AddTicks(352));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("6b912c00-9df3-47a1-a524-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 9, 17, 14, 3, 950, DateTimeKind.Local).AddTicks(358));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 9, 17, 14, 3, 949, DateTimeKind.Local).AddTicks(9749));
        }
    }
}
