using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class AppAttachmentModelModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachmentFiles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 2, 20, 20, 32, 713, DateTimeKind.Local).AddTicks(8960),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 2, 9, 2, 52, 55, DateTimeKind.Local).AddTicks(7063));

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "Sponsors",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "Orders",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "ApplicantAttachment",
                columns: table => new
                {
                    ApplicantsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AttachmentsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantAttachment", x => new { x.ApplicantsId, x.AttachmentsId });
                    table.ForeignKey(
                        name: "FK_ApplicantAttachment_Applicants_ApplicantsId",
                        column: x => x.ApplicantsId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantAttachment_Attachments_AttachmentsId",
                        column: x => x.AttachmentsId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(230));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(255));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(266));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(270));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 719, DateTimeKind.Local).AddTicks(276));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 7, 2, 20, 20, 32, 718, DateTimeKind.Local).AddTicks(9994));

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_AttachmentId",
                table: "Sponsors",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AttachmentId",
                table: "Orders",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantAttachment_AttachmentsId",
                table: "ApplicantAttachment",
                column: "AttachmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Attachments_AttachmentId",
                table: "Orders",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sponsors_Attachments_AttachmentId",
                table: "Sponsors",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Attachments_AttachmentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Sponsors_Attachments_AttachmentId",
                table: "Sponsors");

            migrationBuilder.DropTable(
                name: "ApplicantAttachment");

            migrationBuilder.DropIndex(
                name: "IX_Sponsors_AttachmentId",
                table: "Sponsors");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AttachmentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 2, 9, 2, 52, 55, DateTimeKind.Local).AddTicks(7063),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 7, 2, 20, 20, 32, 713, DateTimeKind.Local).AddTicks(8960));

            migrationBuilder.CreateTable(
                name: "AttachmentFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ApplicantId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    FolderName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttachmentFiles_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
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

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentFiles_ApplicantId",
                table: "AttachmentFiles",
                column: "ApplicantId");
        }
    }
}
