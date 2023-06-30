using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class OrderEmployeeRelMod202306300444 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Applicants_EmployeeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 30, 16, 50, 40, 728, DateTimeKind.Local).AddTicks(6353),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 29, 15, 38, 0, 812, DateTimeKind.Local).AddTicks(3200));

            migrationBuilder.AddColumn<int>(
                name: "ExpiryInterval",
                table: "ProcessDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Applicants",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

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

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_OrderId",
                table: "Applicants",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Orders_OrderId",
                table: "Applicants",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Orders_OrderId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_OrderId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "ExpiryInterval",
                table: "ProcessDefinitions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Applicants");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 29, 15, 38, 0, 812, DateTimeKind.Local).AddTicks(3200),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 30, 16, 50, 40, 728, DateTimeKind.Local).AddTicks(6353));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Orders",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 29, 15, 38, 0, 818, DateTimeKind.Local).AddTicks(7378));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 29, 15, 38, 0, 818, DateTimeKind.Local).AddTicks(7393));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 29, 15, 38, 0, 818, DateTimeKind.Local).AddTicks(7401));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 29, 15, 38, 0, 818, DateTimeKind.Local).AddTicks(7408));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 29, 15, 38, 0, 818, DateTimeKind.Local).AddTicks(7413));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 29, 15, 38, 0, 818, DateTimeKind.Local).AddTicks(7422));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 29, 15, 38, 0, 818, DateTimeKind.Local).AddTicks(7116));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Applicants_EmployeeId",
                table: "Orders",
                column: "EmployeeId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
