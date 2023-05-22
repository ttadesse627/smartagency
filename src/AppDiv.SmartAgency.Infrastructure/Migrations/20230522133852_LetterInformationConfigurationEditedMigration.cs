using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class LetterInformationConfigurationEditedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInformation_Addresses_AddressId",
                table: "CompanyInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanySetting_CompanyInformation_CompanyInformationId",
                table: "CompanySetting");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryOperation_CompanyInformation_CompanyInformationId",
                table: "CountryOperation");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryOperation_LookUps_CountryId",
                table: "CountryOperation");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterInformation_CompanyInformation_CompanyInformationId",
                table: "LetterInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterInformation_Partners_PartnerId",
                table: "LetterInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Witnesses_CompanyInformation_CompanyInformationId",
                table: "Witnesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LetterInformation",
                table: "LetterInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryOperation",
                table: "CountryOperation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanySetting",
                table: "CompanySetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyInformation",
                table: "CompanyInformation");

            migrationBuilder.RenameTable(
                name: "LetterInformation",
                newName: "LetterInformations");

            migrationBuilder.RenameTable(
                name: "CountryOperation",
                newName: "CountryOperations");

            migrationBuilder.RenameTable(
                name: "CompanySetting",
                newName: "CompanySettings");

            migrationBuilder.RenameTable(
                name: "CompanyInformation",
                newName: "CompanyInformations");

            migrationBuilder.RenameIndex(
                name: "IX_LetterInformation_PartnerId",
                table: "LetterInformations",
                newName: "IX_LetterInformations_PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_LetterInformation_CompanyInformationId",
                table: "LetterInformations",
                newName: "IX_LetterInformations_CompanyInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryOperation_CountryId",
                table: "CountryOperations",
                newName: "IX_CountryOperations_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryOperation_CompanyInformationId",
                table: "CountryOperations",
                newName: "IX_CountryOperations_CompanyInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySetting_CompanyInformationId",
                table: "CompanySettings",
                newName: "IX_CompanySettings_CompanyInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyInformation_AddressId",
                table: "CompanyInformations",
                newName: "IX_CompanyInformations_AddressId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 22, 16, 38, 51, 240, DateTimeKind.Local).AddTicks(7539),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 22, 12, 37, 30, 416, DateTimeKind.Local).AddTicks(2826));

            migrationBuilder.AddColumn<string>(
                name: "SubCity",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SubCityArabic",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "WoredaArabic",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LetterInformations",
                table: "LetterInformations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryOperations",
                table: "CountryOperations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanySettings",
                table: "CompanySettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyInformations",
                table: "CompanyInformations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInformations_Addresses_AddressId",
                table: "CompanyInformations",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySettings_CompanyInformations_CompanyInformationId",
                table: "CompanySettings",
                column: "CompanyInformationId",
                principalTable: "CompanyInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryOperations_CompanyInformations_CompanyInformationId",
                table: "CountryOperations",
                column: "CompanyInformationId",
                principalTable: "CompanyInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryOperations_LookUps_CountryId",
                table: "CountryOperations",
                column: "CountryId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterInformations_CompanyInformations_CompanyInformationId",
                table: "LetterInformations",
                column: "CompanyInformationId",
                principalTable: "CompanyInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterInformations_Partners_PartnerId",
                table: "LetterInformations",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Witnesses_CompanyInformations_CompanyInformationId",
                table: "Witnesses",
                column: "CompanyInformationId",
                principalTable: "CompanyInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInformations_Addresses_AddressId",
                table: "CompanyInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanySettings_CompanyInformations_CompanyInformationId",
                table: "CompanySettings");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryOperations_CompanyInformations_CompanyInformationId",
                table: "CountryOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryOperations_LookUps_CountryId",
                table: "CountryOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterInformations_CompanyInformations_CompanyInformationId",
                table: "LetterInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_LetterInformations_Partners_PartnerId",
                table: "LetterInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Witnesses_CompanyInformations_CompanyInformationId",
                table: "Witnesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LetterInformations",
                table: "LetterInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryOperations",
                table: "CountryOperations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanySettings",
                table: "CompanySettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyInformations",
                table: "CompanyInformations");

            migrationBuilder.DropColumn(
                name: "SubCity",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "SubCityArabic",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "WoredaArabic",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "LetterInformations",
                newName: "LetterInformation");

            migrationBuilder.RenameTable(
                name: "CountryOperations",
                newName: "CountryOperation");

            migrationBuilder.RenameTable(
                name: "CompanySettings",
                newName: "CompanySetting");

            migrationBuilder.RenameTable(
                name: "CompanyInformations",
                newName: "CompanyInformation");

            migrationBuilder.RenameIndex(
                name: "IX_LetterInformations_PartnerId",
                table: "LetterInformation",
                newName: "IX_LetterInformation_PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_LetterInformations_CompanyInformationId",
                table: "LetterInformation",
                newName: "IX_LetterInformation_CompanyInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryOperations_CountryId",
                table: "CountryOperation",
                newName: "IX_CountryOperation_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_CountryOperations_CompanyInformationId",
                table: "CountryOperation",
                newName: "IX_CountryOperation_CompanyInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySettings_CompanyInformationId",
                table: "CompanySetting",
                newName: "IX_CompanySetting_CompanyInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyInformations_AddressId",
                table: "CompanyInformation",
                newName: "IX_CompanyInformation_AddressId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 22, 12, 37, 30, 416, DateTimeKind.Local).AddTicks(2826),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 22, 16, 38, 51, 240, DateTimeKind.Local).AddTicks(7539));

            migrationBuilder.AddPrimaryKey(
                name: "PK_LetterInformation",
                table: "LetterInformation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryOperation",
                table: "CountryOperation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanySetting",
                table: "CompanySetting",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyInformation",
                table: "CompanyInformation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInformation_Addresses_AddressId",
                table: "CompanyInformation",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySetting_CompanyInformation_CompanyInformationId",
                table: "CompanySetting",
                column: "CompanyInformationId",
                principalTable: "CompanyInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryOperation_CompanyInformation_CompanyInformationId",
                table: "CountryOperation",
                column: "CompanyInformationId",
                principalTable: "CompanyInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CountryOperation_LookUps_CountryId",
                table: "CountryOperation",
                column: "CountryId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterInformation_CompanyInformation_CompanyInformationId",
                table: "LetterInformation",
                column: "CompanyInformationId",
                principalTable: "CompanyInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LetterInformation_Partners_PartnerId",
                table: "LetterInformation",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Witnesses_CompanyInformation_CompanyInformationId",
                table: "Witnesses",
                column: "CompanyInformationId",
                principalTable: "CompanyInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
