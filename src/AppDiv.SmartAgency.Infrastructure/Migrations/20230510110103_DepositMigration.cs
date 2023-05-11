using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class DepositMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "Applicant");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 10, 14, 1, 2, 106, DateTimeKind.Local).AddTicks(2655),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 9, 15, 18, 58, 713, DateTimeKind.Local).AddTicks(3596));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "Deposits",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "PassportNumber",
                table: "Applicant",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Health",
                table: "Applicant",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Complexion",
                table: "Applicant",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_ApplicantId",
                table: "Deposits",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deposits_Applicant_ApplicantId",
                table: "Deposits",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deposits_Applicant_ApplicantId",
                table: "Deposits");

            migrationBuilder.DropIndex(
                name: "IX_Deposits_ApplicantId",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Deposits");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 9, 15, 18, 58, 713, DateTimeKind.Local).AddTicks(3596),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 10, 14, 1, 2, 106, DateTimeKind.Local).AddTicks(2655));

            migrationBuilder.UpdateData(
                table: "Applicant",
                keyColumn: "PassportNumber",
                keyValue: null,
                column: "PassportNumber",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PassportNumber",
                table: "Applicant",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Applicant",
                keyColumn: "Health",
                keyValue: null,
                column: "Health",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Health",
                table: "Applicant",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Applicant",
                keyColumn: "Complexion",
                keyValue: null,
                column: "Complexion",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Complexion",
                table: "Applicant",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "Applicant",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
