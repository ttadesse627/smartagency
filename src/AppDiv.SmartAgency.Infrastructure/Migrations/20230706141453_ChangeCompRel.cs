using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class ChangeCompRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Orders_OrderId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "IsTraveled",
                table: "Applicants");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Complaints",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_OrderId",
                table: "Complaints",
                newName: "IX_Complaints_ApplicantId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 6, 17, 14, 53, 342, DateTimeKind.Local).AddTicks(4849),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 4, 18, 46, 25, 311, DateTimeKind.Local).AddTicks(1282));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 6, 17, 14, 53, 347, DateTimeKind.Local).AddTicks(3030));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 6, 17, 14, 53, 347, DateTimeKind.Local).AddTicks(3040));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 6, 17, 14, 53, 347, DateTimeKind.Local).AddTicks(3044));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 6, 17, 14, 53, 347, DateTimeKind.Local).AddTicks(3047));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 6, 17, 14, 53, 347, DateTimeKind.Local).AddTicks(3066));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 6, 17, 14, 53, 347, DateTimeKind.Local).AddTicks(3071));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 6, 17, 14, 53, 347, DateTimeKind.Local).AddTicks(2879));

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Applicants_ApplicantId",
                table: "Complaints",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Applicants_ApplicantId",
                table: "Complaints");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "Complaints",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_ApplicantId",
                table: "Complaints",
                newName: "IX_Complaints_OrderId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 4, 18, 46, 25, 311, DateTimeKind.Local).AddTicks(1282),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 6, 17, 14, 53, 342, DateTimeKind.Local).AddTicks(4849));

            migrationBuilder.AddColumn<bool>(
                name: "IsTraveled",
                table: "Applicants",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 4, 18, 46, 25, 315, DateTimeKind.Local).AddTicks(9643));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 4, 18, 46, 25, 315, DateTimeKind.Local).AddTicks(9656));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 4, 18, 46, 25, 315, DateTimeKind.Local).AddTicks(9661));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 4, 18, 46, 25, 315, DateTimeKind.Local).AddTicks(9664));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 4, 18, 46, 25, 315, DateTimeKind.Local).AddTicks(9667));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 4, 18, 46, 25, 315, DateTimeKind.Local).AddTicks(9682));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 4, 18, 46, 25, 315, DateTimeKind.Local).AddTicks(9426));

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Orders_OrderId",
                table: "Complaints",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
