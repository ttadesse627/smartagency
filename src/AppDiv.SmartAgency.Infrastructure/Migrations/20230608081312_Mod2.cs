using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class Mod2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //     migrationBuilder.DropForeignKey(
            //         name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessId",
            //         table: "ApplicantProcesses");

            //     migrationBuilder.RenameColumn(
            //         name: "ProcessId",
            //         table: "ApplicantProcesses",
            //         newName: "ProcessDefinitionId");

            //     migrationBuilder.RenameIndex(
            //         name: "IX_ApplicantProcesses_ProcessId",
            //         table: "ApplicantProcesses",
            //         newName: "IX_ApplicantProcesses_ProcessDefinitionId");

            //     migrationBuilder.AlterColumn<DateTime>(
            //         name: "CreatedAt",
            //         table: "Suffixes",
            //         type: "datetime(6)",
            //         nullable: false,
            //         defaultValue: new DateTime(2023, 6, 8, 11, 13, 12, 228, DateTimeKind.Local).AddTicks(2535),
            //         oldClrType: typeof(DateTime),
            //         oldType: "datetime(6)",
            //         oldDefaultValue: new DateTime(2023, 6, 6, 17, 35, 23, 409, DateTimeKind.Local).AddTicks(6633));

            //     migrationBuilder.AddForeignKey(
            //         name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessDefinitionId",
            //         table: "ApplicantProcesses",
            //         column: "ProcessDefinitionId",
            //         principalTable: "ProcessDefinitions",
            //         principalColumn: "Id",
            //         onDelete: ReferentialAction.Cascade);
            // }

            // protected override void Down(MigrationBuilder migrationBuilder)
            // {
            //     migrationBuilder.DropForeignKey(
            //         name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessDefinitionId",
            //         table: "ApplicantProcesses");

            //     migrationBuilder.RenameColumn(
            //         name: "ProcessDefinitionId",
            //         table: "ApplicantProcesses",
            //         newName: "ProcessId");

            //     migrationBuilder.RenameIndex(
            //         name: "IX_ApplicantProcesses_ProcessDefinitionId",
            //         table: "ApplicantProcesses",
            //         newName: "IX_ApplicantProcesses_ProcessId");

            //     migrationBuilder.AlterColumn<DateTime>(
            //         name: "CreatedAt",
            //         table: "Suffixes",
            //         type: "datetime(6)",
            //         nullable: false,
            //         defaultValue: new DateTime(2023, 6, 6, 17, 35, 23, 409, DateTimeKind.Local).AddTicks(6633),
            //         oldClrType: typeof(DateTime),
            //         oldType: "datetime(6)",
            //         oldDefaultValue: new DateTime(2023, 6, 8, 11, 13, 12, 228, DateTimeKind.Local).AddTicks(2535));

            //     migrationBuilder.AddForeignKey(
            //         name: "FK_ApplicantProcesses_ProcessDefinitions_ProcessId",
            //         table: "ApplicantProcesses",
            //         column: "ProcessId",
            //         principalTable: "ProcessDefinitions",
            //         principalColumn: "Id",
            //         onDelete: ReferentialAction.Cascade);
        }
    }
}
