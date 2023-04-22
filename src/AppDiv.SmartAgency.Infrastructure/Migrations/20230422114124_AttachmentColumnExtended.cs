using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class AttachmentColumnExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                defaultValue: new DateTime(2023, 4, 22, 14, 41, 24, 482, DateTimeKind.Local).AddTicks(7972),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 21, 20, 24, 30, 847, DateTimeKind.Local).AddTicks(814));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Attachments",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Attachments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Attachments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Attachments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Attachments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "05e7589c-c1f4-497e-9313-1741cd3059cb", "Country" },
                    { "880126a1-5065-48a3-90bc-0e1b397b5f5b", "Skill" },
                    { "b3d1bc05-00a8-497b-9481-eb61b2e747aa", "Award" },
                    { "bff34fb0-1750-4408-b612-e28dc66f481a", "Qualification Type" },
                    { "d6576a1a-413e-4977-b781-dc91d941e91d", "Language" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "05e7589c-c1f4-497e-9313-1741cd3059cb");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "880126a1-5065-48a3-90bc-0e1b397b5f5b");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "b3d1bc05-00a8-497b-9481-eb61b2e747aa");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "bff34fb0-1750-4408-b612-e28dc66f481a");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "d6576a1a-413e-4977-b781-dc91d941e91d");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Attachments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 21, 20, 24, 30, 847, DateTimeKind.Local).AddTicks(814),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 22, 14, 41, 24, 482, DateTimeKind.Local).AddTicks(7972));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Attachments",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

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
    }
}
