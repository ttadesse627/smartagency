using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class ModifyReprModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repersentative_Addresses_RepersentativeAddressId",
                table: "Repersentative");

            migrationBuilder.RenameColumn(
                name: "RepersentativeAddressId",
                table: "Repersentative",
                newName: "RepresentativeAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Repersentative_RepersentativeAddressId",
                table: "Repersentative",
                newName: "IX_Repersentative_RepresentativeAddressId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 16, 23, 5, 37, 631, DateTimeKind.Local).AddTicks(1485),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 16, 17, 17, 15, 9, DateTimeKind.Local).AddTicks(5835));

            migrationBuilder.AddForeignKey(
                name: "FK_Repersentative_Addresses_RepresentativeAddressId",
                table: "Repersentative",
                column: "RepresentativeAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repersentative_Addresses_RepresentativeAddressId",
                table: "Repersentative");

            migrationBuilder.RenameColumn(
                name: "RepresentativeAddressId",
                table: "Repersentative",
                newName: "RepersentativeAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Repersentative_RepresentativeAddressId",
                table: "Repersentative",
                newName: "IX_Repersentative_RepersentativeAddressId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 16, 17, 17, 15, 9, DateTimeKind.Local).AddTicks(5835),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 16, 23, 5, 37, 631, DateTimeKind.Local).AddTicks(1485));

            migrationBuilder.AddForeignKey(
                name: "FK_Repersentative_Addresses_RepersentativeAddressId",
                table: "Repersentative",
                column: "RepersentativeAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
