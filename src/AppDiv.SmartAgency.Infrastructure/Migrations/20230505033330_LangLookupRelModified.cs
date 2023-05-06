using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class LangLookupRelModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Applicant_ApplicantId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LookUps_LanguageLookUpId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_LanguageLookUpId",
                table: "Languages");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("22c13ea0-b487-4395-a3a6-23807dff73ad"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 5, 6, 33, 29, 543, DateTimeKind.Local).AddTicks(1711),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 4, 21, 23, 7, 835, DateTimeKind.Local).AddTicks(6547));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("8aec3c2a-96ba-46ce-8a4b-14cf557fd621"), "Category" });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LanguageLookUpId",
                table: "Languages",
                column: "LanguageLookUpId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Applicant_ApplicantId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_LookUps_LanguageLookUpId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_LanguageLookUpId",
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
                defaultValue: new DateTime(2023, 5, 4, 21, 23, 7, 835, DateTimeKind.Local).AddTicks(6547),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 5, 6, 33, 29, 543, DateTimeKind.Local).AddTicks(1711));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("22c13ea0-b487-4395-a3a6-23807dff73ad"), "Category" });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_LanguageLookUpId",
                table: "Languages",
                column: "LanguageLookUpId",
                unique: true);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
