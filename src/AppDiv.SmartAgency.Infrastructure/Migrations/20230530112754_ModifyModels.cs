using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class ModifyModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "IsRequired",
                table: "Attachments",
                newName: "Required");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Attachments",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Attachments",
                newName: "Type");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 30, 14, 27, 53, 722, DateTimeKind.Local).AddTicks(5083),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 30, 12, 31, 2, 90, DateTimeKind.Local).AddTicks(8541));

            migrationBuilder.CreateTable(
                name: "ApplicantProcess",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProcessId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApplicantId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantProcess_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicantProcess_ProcessDefinitions_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "ProcessDefinitions",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantProcess_ApplicantId",
                table: "ApplicantProcess",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantProcess_ProcessId",
                table: "ApplicantProcess",
                column: "ProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantProcess");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Attachments",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Attachments",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Required",
                table: "Attachments",
                newName: "IsRequired");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 30, 12, 31, 2, 90, DateTimeKind.Local).AddTicks(8541),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 30, 14, 27, 53, 722, DateTimeKind.Local).AddTicks(5083));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Attachments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
