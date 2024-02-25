using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUserProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 2, 25, 5, 2, 37, 105, DateTimeKind.Local).AddTicks(1623),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 26, 14, 5, 27, 430, DateTimeKind.Local).AddTicks(2415));

            migrationBuilder.AlterColumn<string>(
                name: "DepositedBy",
                table: "Deposits",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyNameArabic",
                table: "CompanyInformations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyNameAmharic",
                table: "CompanyInformations",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 25, 5, 2, 37, 122, DateTimeKind.Local).AddTicks(4998));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 25, 5, 2, 37, 122, DateTimeKind.Local).AddTicks(5013));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 25, 5, 2, 37, 122, DateTimeKind.Local).AddTicks(5020));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 25, 5, 2, 37, 122, DateTimeKind.Local).AddTicks(5027));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 25, 5, 2, 37, 122, DateTimeKind.Local).AddTicks(5052));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                columns: new[] { "CreatedAt", "Step" },
                values: new object[] { new DateTime(2024, 2, 25, 5, 2, 37, 122, DateTimeKind.Local).AddTicks(5064), 5 });

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("6b912c00-9df3-47a1-a524-410abf239616"),
                columns: new[] { "CreatedAt", "Step" },
                values: new object[] { new DateTime(2024, 2, 25, 5, 2, 37, 122, DateTimeKind.Local).AddTicks(5070), 6 });

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 25, 5, 2, 37, 122, DateTimeKind.Local).AddTicks(4546));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 26, 14, 5, 27, 430, DateTimeKind.Local).AddTicks(2415),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 2, 25, 5, 2, 37, 105, DateTimeKind.Local).AddTicks(1623));

            migrationBuilder.UpdateData(
                table: "Deposits",
                keyColumn: "DepositedBy",
                keyValue: null,
                column: "DepositedBy",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DepositedBy",
                table: "Deposits",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "CompanyInformations",
                keyColumn: "CompanyNameArabic",
                keyValue: null,
                column: "CompanyNameArabic",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyNameArabic",
                table: "CompanyInformations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "CompanyInformations",
                keyColumn: "CompanyNameAmharic",
                keyValue: null,
                column: "CompanyNameAmharic",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyNameAmharic",
                table: "CompanyInformations",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 26, 14, 5, 27, 439, DateTimeKind.Local).AddTicks(6057));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 26, 14, 5, 27, 439, DateTimeKind.Local).AddTicks(6071));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 26, 14, 5, 27, 439, DateTimeKind.Local).AddTicks(6075));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 26, 14, 5, 27, 439, DateTimeKind.Local).AddTicks(6080));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 26, 14, 5, 27, 439, DateTimeKind.Local).AddTicks(6085));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                columns: new[] { "CreatedAt", "Step" },
                values: new object[] { new DateTime(2023, 7, 26, 14, 5, 27, 439, DateTimeKind.Local).AddTicks(6091), 2 });

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("6b912c00-9df3-47a1-a524-410abf239616"),
                columns: new[] { "CreatedAt", "Step" },
                values: new object[] { new DateTime(2023, 7, 26, 14, 5, 27, 439, DateTimeKind.Local).AddTicks(6095), 3 });

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 26, 14, 5, 27, 439, DateTimeKind.Local).AddTicks(5643));
        }
    }
}
