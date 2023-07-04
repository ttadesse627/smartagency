using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class AttachmentFileModified202307020900 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentFiles_Attachments_AttachmentId",
                table: "AttachmentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentFiles_Orders_OrderId",
                table: "AttachmentFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_AttachmentFiles_AttachmentFileId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_AttachmentFileId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_AttachmentFiles_AttachmentId",
                table: "AttachmentFiles");

            migrationBuilder.DropIndex(
                name: "IX_AttachmentFiles_OrderId",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "AttachmentFileId",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "AttachmentFiles");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "AttachmentFiles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 2, 9, 2, 52, 55, DateTimeKind.Local).AddTicks(7063),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 30, 16, 50, 40, 728, DateTimeKind.Local).AddTicks(6353));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVisa",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FolderName",
                table: "AttachmentFiles",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 9, 2, 52, 62, DateTimeKind.Local).AddTicks(91));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 9, 2, 52, 62, DateTimeKind.Local).AddTicks(103));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 9, 2, 52, 62, DateTimeKind.Local).AddTicks(108));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 9, 2, 52, 62, DateTimeKind.Local).AddTicks(112));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 9, 2, 52, 62, DateTimeKind.Local).AddTicks(131));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 9, 2, 52, 62, DateTimeKind.Local).AddTicks(137));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 9, 2, 52, 61, DateTimeKind.Local).AddTicks(9895));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfVisa",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FolderName",
                table: "AttachmentFiles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 30, 16, 50, 40, 728, DateTimeKind.Local).AddTicks(6353),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 2, 9, 2, 52, 55, DateTimeKind.Local).AddTicks(7063));

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentFileId",
                table: "Sponsors",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "AttachmentFiles",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

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

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
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

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "AttachmentFiles",
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
                name: "IX_Sponsors_AttachmentFileId",
                table: "Sponsors",
                column: "AttachmentFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentFiles_AttachmentId",
                table: "AttachmentFiles",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentFiles_OrderId",
                table: "AttachmentFiles",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentFiles_Attachments_AttachmentId",
                table: "AttachmentFiles",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentFiles_Orders_OrderId",
                table: "AttachmentFiles",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_AttachmentFiles_AttachmentFileId",
                table: "Sponsors",
                column: "AttachmentFileId",
                principalTable: "AttachmentFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
