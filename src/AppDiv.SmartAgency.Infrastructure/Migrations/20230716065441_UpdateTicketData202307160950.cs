using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class UpdateTicketData202307160950 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 16, 9, 54, 40, 372, DateTimeKind.Local).AddTicks(9169),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 15, 16, 55, 11, 164, DateTimeKind.Local).AddTicks(1826));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 16, 9, 54, 40, 382, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 16, 9, 54, 40, 382, DateTimeKind.Local).AddTicks(6212));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 16, 9, 54, 40, 382, DateTimeKind.Local).AddTicks(6223));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 16, 9, 54, 40, 382, DateTimeKind.Local).AddTicks(6233));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 16, 9, 54, 40, 382, DateTimeKind.Local).AddTicks(6242));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                columns: new[] { "CreatedAt", "RequestApproval" },
                values: new object[] { new DateTime(2023, 7, 16, 9, 54, 40, 382, DateTimeKind.Local).AddTicks(6268), false });

            migrationBuilder.InsertData(
                table: "ProcessDefinitions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "ExpiryInterval", "ModifiedAt", "ModifiedBy", "Name", "ProcessId", "RequestApproval", "Step" },
                values: new object[] { new Guid("6b912c00-9df3-47a1-a524-410abf239616"), new DateTime(2023, 7, 16, 9, 54, 40, 382, DateTimeKind.Local).AddTicks(6299), null, 0, null, null, "Traveled", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), true, 3 });

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 16, 9, 54, 40, 382, DateTimeKind.Local).AddTicks(5805));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("6b912c00-9df3-47a1-a524-410abf239616"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 15, 16, 55, 11, 164, DateTimeKind.Local).AddTicks(1826),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 16, 9, 54, 40, 372, DateTimeKind.Local).AddTicks(9169));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 15, 16, 55, 11, 172, DateTimeKind.Local).AddTicks(2577));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 15, 16, 55, 11, 172, DateTimeKind.Local).AddTicks(2590));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 15, 16, 55, 11, 172, DateTimeKind.Local).AddTicks(2597));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 15, 16, 55, 11, 172, DateTimeKind.Local).AddTicks(2603));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 15, 16, 55, 11, 172, DateTimeKind.Local).AddTicks(2609));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                columns: new[] { "CreatedAt", "RequestApproval" },
                values: new object[] { new DateTime(2023, 7, 15, 16, 55, 11, 172, DateTimeKind.Local).AddTicks(2634), true });

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 15, 16, 55, 11, 172, DateTimeKind.Local).AddTicks(2275));
        }
    }
}
