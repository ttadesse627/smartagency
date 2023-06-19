using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class ProcessMigrationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessId",
                table: "ApplicantProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Repersentative_Addresses_AddressId",
                table: "Repersentative");

            migrationBuilder.DropForeignKey(
                name: "FK_Repersentative_Applicants_ApplicantId",
                table: "Repersentative");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repersentative",
                table: "Repersentative");

            migrationBuilder.DropIndex(
                name: "IX_Repersentative_AddressId",
                table: "Repersentative");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Witnesses");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Witnesses");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Witnesses");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Witnesses");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Witnesses");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Repersentative");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Repersentative");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Repersentative");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Repersentative");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Repersentative");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Repersentative");

            migrationBuilder.RenameTable(
                name: "Repersentative",
                newName: "Representatives");

            migrationBuilder.RenameColumn(
                name: "ArabicFullName",
                table: "EmergencyContacts",
                newName: "NameOfContactPerson");

            migrationBuilder.RenameColumn(
                name: "ProcessId",
                table: "ApplicantProcesses",
                newName: "ProcessDefinitionId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicantProcesses_ProcessId",
                table: "ApplicantProcesses",
                newName: "IX_ApplicantProcesses_ProcessDefinitionId");

            migrationBuilder.RenameColumn(
                name: "Addres",
                table: "Addresses",
                newName: "Adress");

            migrationBuilder.RenameIndex(
                name: "IX_Repersentative_ApplicantId",
                table: "Representatives",
                newName: "IX_Representatives_ApplicantId");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Witnesses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 10, 11, 16, 18, 88, DateTimeKind.Local).AddTicks(9403),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 6, 17, 35, 23, 409, DateTimeKind.Local).AddTicks(6633));

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "EmergencyContacts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Beneficiaries",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "RepresentativeId",
                table: "Addresses",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Representatives",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Representatives",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "Representatives",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Kebele",
                table: "Representatives",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Representatives",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Woreda",
                table: "Representatives",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Zone",
                table: "Representatives",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Representatives",
                table: "Representatives",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RepresentativeId",
                table: "Addresses",
                column: "RepresentativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Representatives_RepresentativeId",
                table: "Addresses",
                column: "RepresentativeId",
                principalTable: "Representatives",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessDefinitionId",
                table: "ApplicantProcesses",
                column: "ProcessDefinitionId",
                principalTable: "ProcessDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Representatives_Applicants_ApplicantId",
                table: "Representatives",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Representatives_RepresentativeId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessDefinitionId",
                table: "ApplicantProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_Representatives_Applicants_ApplicantId",
                table: "Representatives");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_RepresentativeId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Representatives",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Witnesses");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "RepresentativeId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "Kebele",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "Woreda",
                table: "Representatives");

            migrationBuilder.DropColumn(
                name: "Zone",
                table: "Representatives");

            migrationBuilder.RenameTable(
                name: "Representatives",
                newName: "Repersentative");

            migrationBuilder.RenameColumn(
                name: "NameOfContactPerson",
                table: "EmergencyContacts",
                newName: "ArabicFullName");

            migrationBuilder.RenameColumn(
                name: "ProcessDefinitionId",
                table: "ApplicantProcesses",
                newName: "ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicantProcesses_ProcessDefinitionId",
                table: "ApplicantProcesses",
                newName: "IX_ApplicantProcesses_ProcessId");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Addresses",
                newName: "Addres");

            migrationBuilder.RenameIndex(
                name: "IX_Representatives_ApplicantId",
                table: "Repersentative",
                newName: "IX_Repersentative_ApplicantId");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Witnesses",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Witnesses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Witnesses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Witnesses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Witnesses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 6, 17, 35, 23, 409, DateTimeKind.Local).AddTicks(6633),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 10, 11, 16, 18, 88, DateTimeKind.Local).AddTicks(9403));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "EmergencyContacts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "EmergencyContacts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "EmergencyContacts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Beneficiaries",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Beneficiaries",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Beneficiaries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Beneficiaries",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Beneficiaries",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Repersentative",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Repersentative",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Repersentative",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Repersentative",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Repersentative",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Repersentative",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repersentative",
                table: "Repersentative",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Repersentative_AddressId",
                table: "Repersentative",
                column: "AddressId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessId",
                table: "ApplicantProcesses",
                column: "ProcessId",
                principalTable: "ProcessDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repersentative_Addresses_AddressId",
                table: "Repersentative",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repersentative_Applicants_ApplicantId",
                table: "Repersentative",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
