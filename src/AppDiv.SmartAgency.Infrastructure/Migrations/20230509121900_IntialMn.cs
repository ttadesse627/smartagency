using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class IntialMn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineApplicants_LookUps_MartialStatusId",
                table: "OnlineApplicants");

            migrationBuilder.RenameColumn(
                name: "MartialStatusId",
                table: "OnlineApplicants",
                newName: "MaritalStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_OnlineApplicants_MartialStatusId",
                table: "OnlineApplicants",
                newName: "IX_OnlineApplicants_MaritalStatusId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 9, 15, 18, 58, 713, DateTimeKind.Local).AddTicks(3596),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 9, 15, 6, 12, 509, DateTimeKind.Local).AddTicks(6930));

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineApplicants_LookUps_MaritalStatusId",
                table: "OnlineApplicants",
                column: "MaritalStatusId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineApplicants_LookUps_MaritalStatusId",
                table: "OnlineApplicants");

            migrationBuilder.RenameColumn(
                name: "MaritalStatusId",
                table: "OnlineApplicants",
                newName: "MartialStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_OnlineApplicants_MaritalStatusId",
                table: "OnlineApplicants",
                newName: "IX_OnlineApplicants_MartialStatusId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 9, 15, 6, 12, 509, DateTimeKind.Local).AddTicks(6930),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 9, 15, 18, 58, 713, DateTimeKind.Local).AddTicks(3596));

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineApplicants_LookUps_MartialStatusId",
                table: "OnlineApplicants",
                column: "MartialStatusId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
