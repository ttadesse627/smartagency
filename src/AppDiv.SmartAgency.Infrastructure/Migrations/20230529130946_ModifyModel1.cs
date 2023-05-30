using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class ModifyModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8aec3c2a-96ba-46ce-8a4b-14cf557fd621"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 16, 9, 45, 851, DateTimeKind.Local).AddTicks(6328),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 29, 10, 6, 33, 162, DateTimeKind.Local).AddTicks(7608));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AttachmentFiles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "AttachmentFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "AttachmentFiles",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "AttachmentFiles",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AttachmentFiles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 10, 6, 33, 162, DateTimeKind.Local).AddTicks(7608),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 29, 16, 9, 45, 851, DateTimeKind.Local).AddTicks(6328));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("8aec3c2a-96ba-46ce-8a4b-14cf557fd621"), "Category" });
        }
    }
}
