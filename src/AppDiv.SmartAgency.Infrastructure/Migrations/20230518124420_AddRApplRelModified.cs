using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class AddRApplRelModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Addresses_AddressId",
                table: "Applicants");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 18, 15, 44, 19, 746, DateTimeKind.Local).AddTicks(204),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 18, 13, 40, 58, 519, DateTimeKind.Local).AddTicks(8282));

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Addresses_AddressId",
                table: "Applicants",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Addresses_AddressId",
                table: "Applicants");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 18, 13, 40, 58, 519, DateTimeKind.Local).AddTicks(8282),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 18, 15, 44, 19, 746, DateTimeKind.Local).AddTicks(204));

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Addresses_AddressId",
                table: "Applicants",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
