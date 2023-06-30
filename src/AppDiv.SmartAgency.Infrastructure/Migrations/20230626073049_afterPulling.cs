using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class afterPulling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enjazs_Sponsors_SponsorId",
                table: "Enjazs");

            migrationBuilder.RenameColumn(
                name: "SponsorId",
                table: "Enjazs",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Enjazs_SponsorId",
                table: "Enjazs",
                newName: "IX_Enjazs_OrderId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 26, 10, 30, 49, 382, DateTimeKind.Local).AddTicks(7004),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 18, 13, 2, 31, 657, DateTimeKind.Local).AddTicks(7365));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationNumber",
                table: "Enjazs",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 10, 30, 49, 387, DateTimeKind.Local).AddTicks(6310));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 10, 30, 49, 387, DateTimeKind.Local).AddTicks(6325));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 10, 30, 49, 387, DateTimeKind.Local).AddTicks(6329));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 10, 30, 49, 387, DateTimeKind.Local).AddTicks(6343));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 10, 30, 49, 387, DateTimeKind.Local).AddTicks(6347));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 10, 30, 49, 387, DateTimeKind.Local).AddTicks(6353));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 26, 10, 30, 49, 387, DateTimeKind.Local).AddTicks(6140));

            migrationBuilder.AddForeignKey(
                name: "FK_Enjazs_Orders_OrderId",
                table: "Enjazs",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enjazs_Orders_OrderId",
                table: "Enjazs");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Enjazs",
                newName: "SponsorId");

            migrationBuilder.RenameIndex(
                name: "IX_Enjazs_OrderId",
                table: "Enjazs",
                newName: "IX_Enjazs_SponsorId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 18, 13, 2, 31, 657, DateTimeKind.Local).AddTicks(7365),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 26, 10, 30, 49, 382, DateTimeKind.Local).AddTicks(7004));

            migrationBuilder.UpdateData(
                table: "Enjazs",
                keyColumn: "ApplicationNumber",
                keyValue: null,
                column: "ApplicationNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationNumber",
                table: "Enjazs",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 18, 13, 2, 31, 667, DateTimeKind.Local).AddTicks(4098));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 18, 13, 2, 31, 667, DateTimeKind.Local).AddTicks(4134));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 18, 13, 2, 31, 667, DateTimeKind.Local).AddTicks(4144));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 18, 13, 2, 31, 667, DateTimeKind.Local).AddTicks(4154));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 18, 13, 2, 31, 667, DateTimeKind.Local).AddTicks(4221));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 18, 13, 2, 31, 667, DateTimeKind.Local).AddTicks(4234));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 18, 13, 2, 31, 667, DateTimeKind.Local).AddTicks(3662));

            migrationBuilder.AddForeignKey(
                name: "FK_Enjazs_Sponsors_SponsorId",
                table: "Enjazs",
                column: "SponsorId",
                principalTable: "Sponsors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
