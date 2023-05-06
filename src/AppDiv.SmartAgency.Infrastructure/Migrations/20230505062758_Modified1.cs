using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class Modified1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_Applicant_ApplicantId",
                table: "Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Applicant_ApplicantId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LookUps_LanguageLookUpId",
                table: "Languages");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8aec3c2a-96ba-46ce-8a4b-14cf557fd621"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 5, 9, 27, 57, 477, DateTimeKind.Local).AddTicks(3332),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 5, 6, 33, 29, 543, DateTimeKind.Local).AddTicks(1711));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("dce16473-7a26-42b2-ab79-1d0c48079ccd"), "Category" });

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_Applicant_ApplicantId",
                table: "Beneficiary",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Applicant_ApplicantId",
                table: "Languages",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_LookUps_LanguageLookUpId",
                table: "Languages",
                column: "LanguageLookUpId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_Applicant_ApplicantId",
                table: "Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Applicant_ApplicantId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LookUps_LanguageLookUpId",
                table: "Languages");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("dce16473-7a26-42b2-ab79-1d0c48079ccd"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 5, 6, 33, 29, 543, DateTimeKind.Local).AddTicks(1711),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 5, 9, 27, 57, 477, DateTimeKind.Local).AddTicks(3332));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("8aec3c2a-96ba-46ce-8a4b-14cf557fd621"), "Category" });

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_Applicant_ApplicantId",
                table: "Beneficiary",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Applicant_ApplicantId",
                table: "Languages",
                column: "ApplicantId",
                principalTable: "Applicant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_LookUps_LanguageLookUpId",
                table: "Languages",
                column: "LanguageLookUpId",
                principalTable: "LookUps",
                principalColumn: "Id");
        }
    }
}
