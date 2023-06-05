using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class ApplicantProcessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantProcess_Applicants_ApplicantId",
                table: "ApplicantProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantProcess_ProcessDefinitions_ProcessId",
                table: "ApplicantProcess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicantProcess",
                table: "ApplicantProcess");

            migrationBuilder.RenameTable(
                name: "ApplicantProcess",
                newName: "ApplicantProcesses");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicantProcess_ProcessId",
                table: "ApplicantProcesses",
                newName: "IX_ApplicantProcesses_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicantProcess_ApplicantId",
                table: "ApplicantProcesses",
                newName: "IX_ApplicantProcesses_ApplicantId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 3, 22, 54, 42, 348, DateTimeKind.Local).AddTicks(3270),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 2, 23, 31, 46, 820, DateTimeKind.Local).AddTicks(3606));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicantProcesses",
                table: "ApplicantProcesses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantProcesses_Applicants_ApplicantId",
                table: "ApplicantProcesses",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessId",
                table: "ApplicantProcesses",
                column: "ProcessId",
                principalTable: "ProcessDefinitions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantProcesses_Applicants_ApplicantId",
                table: "ApplicantProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessId",
                table: "ApplicantProcesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicantProcesses",
                table: "ApplicantProcesses");

            migrationBuilder.RenameTable(
                name: "ApplicantProcesses",
                newName: "ApplicantProcess");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicantProcesses_ProcessId",
                table: "ApplicantProcess",
                newName: "IX_ApplicantProcess_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicantProcesses_ApplicantId",
                table: "ApplicantProcess",
                newName: "IX_ApplicantProcess_ApplicantId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 2, 23, 31, 46, 820, DateTimeKind.Local).AddTicks(3606),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 3, 22, 54, 42, 348, DateTimeKind.Local).AddTicks(3270));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicantProcess",
                table: "ApplicantProcess",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantProcess_Applicants_ApplicantId",
                table: "ApplicantProcess",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantProcess_ProcessDefinitions_ProcessId",
                table: "ApplicantProcess",
                column: "ProcessId",
                principalTable: "ProcessDefinitions",
                principalColumn: "Id");
        }
    }
}
