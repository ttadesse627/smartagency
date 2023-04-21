using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class AddAttachmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "3c51d531-0314-4e3f-943f-f8c1b040cbff");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "89a6b010-eb44-44dd-9028-fec165887693");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "b886958a-8fca-4361-b673-4ab18dbcc5c2");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "c419e08d-dec2-4496-a5aa-bf8345146563");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "ef3a922e-0e9a-40dd-b246-ff00ce7ec6e9");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 21, 20, 24, 30, 847, DateTimeKind.Local).AddTicks(814),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 21, 18, 28, 44, 354, DateTimeKind.Local).AddTicks(2146));

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ShowOnCv = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "24463a3a-3dcd-4095-9a20-9d664cdd2c4b", "Award" },
                    { "3a04c9e6-a6ec-41bb-8c5e-949343408582", "Language" },
                    { "91bd7504-7209-4b84-95a1-db4fb9a64618", "Skill" },
                    { "a3526770-3c0e-4e7f-8330-5faa4c913aa0", "Country" },
                    { "d9ea70c6-3252-4e8f-a31a-1915ce495197", "Qualification Type" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "24463a3a-3dcd-4095-9a20-9d664cdd2c4b");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "3a04c9e6-a6ec-41bb-8c5e-949343408582");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "91bd7504-7209-4b84-95a1-db4fb9a64618");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "a3526770-3c0e-4e7f-8330-5faa4c913aa0");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "d9ea70c6-3252-4e8f-a31a-1915ce495197");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 21, 18, 28, 44, 354, DateTimeKind.Local).AddTicks(2146),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 21, 20, 24, 30, 847, DateTimeKind.Local).AddTicks(814));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "3c51d531-0314-4e3f-943f-f8c1b040cbff", "Skill" },
                    { "89a6b010-eb44-44dd-9028-fec165887693", "Language" },
                    { "b886958a-8fca-4361-b673-4ab18dbcc5c2", "Country" },
                    { "c419e08d-dec2-4496-a5aa-bf8345146563", "Qualification Type" },
                    { "ef3a922e-0e9a-40dd-b246-ff00ce7ec6e9", "Award" }
                });
        }
    }
}
