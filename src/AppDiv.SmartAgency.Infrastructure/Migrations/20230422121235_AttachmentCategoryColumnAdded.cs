using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class AttachmentCategoryColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 15, 12, 34, 659, DateTimeKind.Local).AddTicks(668),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 22, 14, 41, 24, 482, DateTimeKind.Local).AddTicks(7972));

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Attachments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "1647efce-7321-4690-b3d0-7c259bc9147f", "Language" },
                    { "29247167-6ad8-41fb-86ed-4b118140f52b", "Country" },
                    { "8228545d-8974-4919-b64a-75c96ea1d148", "Award" },
                    { "da7f1382-9541-47ae-8927-a48e0bae7763", "Qualification Type" },
                    { "e28a4af4-668a-4fd7-a28f-8d0842984700", "Skill" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "1647efce-7321-4690-b3d0-7c259bc9147f");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "29247167-6ad8-41fb-86ed-4b118140f52b");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "8228545d-8974-4919-b64a-75c96ea1d148");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "da7f1382-9541-47ae-8927-a48e0bae7763");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "e28a4af4-668a-4fd7-a28f-8d0842984700");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Attachments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 14, 41, 24, 482, DateTimeKind.Local).AddTicks(7972),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 22, 15, 12, 34, 659, DateTimeKind.Local).AddTicks(668));

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
    }
}
