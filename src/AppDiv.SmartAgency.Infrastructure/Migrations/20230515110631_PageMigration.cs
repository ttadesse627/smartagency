using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class PageMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_LookUps_AddressCountryId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantWitness_Witness_ApplicantWitnessesId",
                table: "ApplicantWitness");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_Applicants_BeneficiaryApplicantId",
                table: "Beneficiary");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiary_LookUps_BeneficiaryRelationshipId",
                table: "Beneficiary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Witness",
                table: "Witness");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beneficiary",
                table: "Beneficiary");

            migrationBuilder.RenameTable(
                name: "Witness",
                newName: "Witnesses");

            migrationBuilder.RenameTable(
                name: "Beneficiary",
                newName: "Beneficiaries");

            migrationBuilder.RenameColumn(
                name: "AddressCountryId",
                table: "Addresses",
                newName: "AddressRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AddressCountryId",
                table: "Addresses",
                newName: "IX_Addresses_AddressRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiary_BeneficiaryRelationshipId",
                table: "Beneficiaries",
                newName: "IX_Beneficiaries_BeneficiaryRelationshipId");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiary_BeneficiaryApplicantId",
                table: "Beneficiaries",
                newName: "IX_Beneficiaries_BeneficiaryApplicantId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 15, 14, 6, 30, 64, DateTimeKind.Local).AddTicks(347),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 12, 13, 48, 52, 44, DateTimeKind.Local).AddTicks(8906));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplExperienceId",
                table: "Applicants",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Addres",
                table: "Addresses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AlternativePhone",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DistrictArabic",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OfficePhone",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "StreetArabic",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Witnesses",
                table: "Witnesses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beneficiaries",
                table: "Beneficiaries",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Link = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PageContent = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_LookUps_AddressRegionId",
                table: "Addresses",
                column: "AddressRegionId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantWitness_Witnesses_ApplicantWitnessesId",
                table: "ApplicantWitness",
                column: "ApplicantWitnessesId",
                principalTable: "Witnesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_Applicants_BeneficiaryApplicantId",
                table: "Beneficiaries",
                column: "BeneficiaryApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_LookUps_BeneficiaryRelationshipId",
                table: "Beneficiaries",
                column: "BeneficiaryRelationshipId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_LookUps_AddressRegionId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantWitness_Witnesses_ApplicantWitnessesId",
                table: "ApplicantWitness");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_Applicants_BeneficiaryApplicantId",
                table: "Beneficiaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_LookUps_BeneficiaryRelationshipId",
                table: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Witnesses",
                table: "Witnesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Beneficiaries",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "ApplExperienceId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "Addres",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AlternativePhone",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "DistrictArabic",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "OfficePhone",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StreetArabic",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Witnesses",
                newName: "Witness");

            migrationBuilder.RenameTable(
                name: "Beneficiaries",
                newName: "Beneficiary");

            migrationBuilder.RenameColumn(
                name: "AddressRegionId",
                table: "Addresses",
                newName: "AddressCountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AddressRegionId",
                table: "Addresses",
                newName: "IX_Addresses_AddressCountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiaries_BeneficiaryRelationshipId",
                table: "Beneficiary",
                newName: "IX_Beneficiary_BeneficiaryRelationshipId");

            migrationBuilder.RenameIndex(
                name: "IX_Beneficiaries_BeneficiaryApplicantId",
                table: "Beneficiary",
                newName: "IX_Beneficiary_BeneficiaryApplicantId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 12, 13, 48, 52, 44, DateTimeKind.Local).AddTicks(8906),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 15, 14, 6, 30, 64, DateTimeKind.Local).AddTicks(347));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Witness",
                table: "Witness",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Beneficiary",
                table: "Beneficiary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_LookUps_AddressCountryId",
                table: "Addresses",
                column: "AddressCountryId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantWitness_Witness_ApplicantWitnessesId",
                table: "ApplicantWitness",
                column: "ApplicantWitnessesId",
                principalTable: "Witness",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_Applicants_BeneficiaryApplicantId",
                table: "Beneficiary",
                column: "BeneficiaryApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiary_LookUps_BeneficiaryRelationshipId",
                table: "Beneficiary",
                column: "BeneficiaryRelationshipId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
