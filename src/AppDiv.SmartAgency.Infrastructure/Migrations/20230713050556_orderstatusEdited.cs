using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class orderstatusEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Enjazs_Orders_OrderId",
                table: "Enjazs");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Enjazs",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_Enjazs_OrderId",
                table: "Enjazs",
                newName: "IX_Enjazs_ApplicantId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 13, 8, 5, 55, 862, DateTimeKind.Local).AddTicks(1941),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 7, 18, 4, 37, 296, DateTimeKind.Local).AddTicks(8749));

            migrationBuilder.AlterColumn<string>(
                name: "TransactionCode",
                table: "Enjazs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 13, 8, 5, 55, 867, DateTimeKind.Local).AddTicks(5408));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 13, 8, 5, 55, 867, DateTimeKind.Local).AddTicks(5422));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 13, 8, 5, 55, 867, DateTimeKind.Local).AddTicks(5426));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 13, 8, 5, 55, 867, DateTimeKind.Local).AddTicks(5429));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 13, 8, 5, 55, 867, DateTimeKind.Local).AddTicks(5433));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 13, 8, 5, 55, 867, DateTimeKind.Local).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 13, 8, 5, 55, 867, DateTimeKind.Local).AddTicks(5241));

            migrationBuilder.AddForeignKey(
                name: "FK_Enjazs_Applicants_ApplicantId",
                table: "Enjazs",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enjazs_Applicants_ApplicantId",
                table: "Enjazs");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "Enjazs",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Enjazs_ApplicantId",
                table: "Enjazs",
                newName: "IX_Enjazs_OrderId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 7, 18, 4, 37, 296, DateTimeKind.Local).AddTicks(8749),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 13, 8, 5, 55, 862, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.AlterColumn<int>(
                name: "TransactionCode",
                table: "Enjazs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 7, 18, 4, 37, 304, DateTimeKind.Local).AddTicks(5693));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 7, 18, 4, 37, 304, DateTimeKind.Local).AddTicks(5736));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 7, 18, 4, 37, 304, DateTimeKind.Local).AddTicks(5744));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 7, 18, 4, 37, 304, DateTimeKind.Local).AddTicks(5751));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 7, 18, 4, 37, 304, DateTimeKind.Local).AddTicks(5759));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 7, 18, 4, 37, 304, DateTimeKind.Local).AddTicks(5768));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 7, 18, 4, 37, 304, DateTimeKind.Local).AddTicks(5392));

            migrationBuilder.AddForeignKey(
                name: "FK_Enjazs_Orders_OrderId",
                table: "Enjazs",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
