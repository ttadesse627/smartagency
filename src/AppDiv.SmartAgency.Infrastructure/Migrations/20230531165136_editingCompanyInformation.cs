using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class editingCompanyInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Addresses_AddressId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "LetterBackGround",
                table: "CompanyInformations");

            migrationBuilder.DropColumn(
                name: "LetterLogo",
                table: "CompanyInformations");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Addresses",
                newName: "Addres");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 31, 19, 51, 35, 217, DateTimeKind.Local).AddTicks(757),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 29, 17, 29, 52, 351, DateTimeKind.Local).AddTicks(4172));

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Addresses_AddressId",
                table: "Partners",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Addresses_AddressId",
                table: "Partners");

            migrationBuilder.RenameColumn(
                name: "Addres",
                table: "Addresses",
                newName: "Adress");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 29, 17, 29, 52, 351, DateTimeKind.Local).AddTicks(4172),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 31, 19, 51, 35, 217, DateTimeKind.Local).AddTicks(757));

            migrationBuilder.AddColumn<string>(
                name: "LetterBackGround",
                table: "CompanyInformations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LetterLogo",
                table: "CompanyInformations",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Addresses_AddressId",
                table: "Partners",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
