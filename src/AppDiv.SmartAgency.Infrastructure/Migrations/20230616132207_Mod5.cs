using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class Mod5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_TicketReadies_TicketReadyId",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_TicketRebookRegistrations_TicketRebookRegistratio~",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_TicketRebooks_TicketRebookId",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_TicketRefunds_TicketRefundId",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_TicketRegistrations_TicketRegistrationId",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketReadies_ApplicantProcesses_ApplicantProcessId",
                table: "TicketReadies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRebookRegistrations_ApplicantProcesses_ApplicantProces~",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRebooks_ApplicantProcesses_ApplicantProcessId",
                table: "TicketRebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRefunds_ApplicantProcesses_ApplicantProcessId",
                table: "TicketRefunds");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRegistrations_ApplicantProcesses_ApplicantProcessId",
                table: "TicketRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TraveledApplicant_ApplicantProcesses_ApplicantProcessId",
                table: "TraveledApplicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_TicketReadyId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_TicketRebookId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_TicketRebookRegistrationId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_TicketRefundId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_TicketRegistrationId",
                table: "Applicants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TraveledApplicant",
                table: "TraveledApplicant");

            migrationBuilder.DropColumn(
                name: "TicketReadyId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "TicketRebookId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "TicketRebookRegistrationId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "TicketRefundId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "TicketRegistrationId",
                table: "Applicants");

            migrationBuilder.RenameTable(
                name: "TraveledApplicant",
                newName: "TraveledApplicants");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketRegistrations",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRegistrations_ApplicantProcessId",
                table: "TicketRegistrations",
                newName: "IX_TicketRegistrations_ApplicantId");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketRefunds",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRefunds_ApplicantProcessId",
                table: "TicketRefunds",
                newName: "IX_TicketRefunds_ApplicantId");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketRebooks",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRebooks_ApplicantProcessId",
                table: "TicketRebooks",
                newName: "IX_TicketRebooks_ApplicantId");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketRebookRegistrations",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRebookRegistrations_ApplicantProcessId",
                table: "TicketRebookRegistrations",
                newName: "IX_TicketRebookRegistrations_ApplicantId");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketReadies",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketReadies_ApplicantProcessId",
                table: "TicketReadies",
                newName: "IX_TicketReadies_ApplicantId");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TraveledApplicants",
                newName: "ApplicantId");

            migrationBuilder.RenameIndex(
                name: "IX_TraveledApplicant_ApplicantProcessId",
                table: "TraveledApplicants",
                newName: "IX_TraveledApplicants_ApplicantId");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegiteredDate",
                table: "TicketRegistrations",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 16, 16, 22, 7, 6, DateTimeKind.Local).AddTicks(215),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 14, 13, 9, 57, 497, DateTimeKind.Local).AddTicks(9022));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TraveledApplicants",
                table: "TraveledApplicants",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 16, 16, 22, 7, 19, DateTimeKind.Local).AddTicks(124));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 16, 16, 22, 7, 19, DateTimeKind.Local).AddTicks(151));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 16, 16, 22, 7, 19, DateTimeKind.Local).AddTicks(160));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 16, 16, 22, 7, 19, DateTimeKind.Local).AddTicks(169));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 16, 16, 22, 7, 19, DateTimeKind.Local).AddTicks(178));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 16, 16, 22, 7, 19, DateTimeKind.Local).AddTicks(191));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 16, 16, 22, 7, 18, DateTimeKind.Local).AddTicks(9670));

            migrationBuilder.AddForeignKey(
                name: "FK_TicketReadies_Applicants_ApplicantId",
                table: "TicketReadies",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRebookRegistrations_Applicants_ApplicantId",
                table: "TicketRebookRegistrations",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRebooks_Applicants_ApplicantId",
                table: "TicketRebooks",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRefunds_Applicants_ApplicantId",
                table: "TicketRefunds",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRegistrations_Applicants_ApplicantId",
                table: "TicketRegistrations",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraveledApplicants_Applicants_ApplicantId",
                table: "TraveledApplicants",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketReadies_Applicants_ApplicantId",
                table: "TicketReadies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRebookRegistrations_Applicants_ApplicantId",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRebooks_Applicants_ApplicantId",
                table: "TicketRebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRefunds_Applicants_ApplicantId",
                table: "TicketRefunds");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRegistrations_Applicants_ApplicantId",
                table: "TicketRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TraveledApplicants_Applicants_ApplicantId",
                table: "TraveledApplicants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TraveledApplicants",
                table: "TraveledApplicants");

            migrationBuilder.DropColumn(
                name: "RegiteredDate",
                table: "TicketRegistrations");

            migrationBuilder.RenameTable(
                name: "TraveledApplicants",
                newName: "TraveledApplicant");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "TicketRegistrations",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRegistrations_ApplicantId",
                table: "TicketRegistrations",
                newName: "IX_TicketRegistrations_ApplicantProcessId");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "TicketRefunds",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRefunds_ApplicantId",
                table: "TicketRefunds",
                newName: "IX_TicketRefunds_ApplicantProcessId");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "TicketRebooks",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRebooks_ApplicantId",
                table: "TicketRebooks",
                newName: "IX_TicketRebooks_ApplicantProcessId");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "TicketRebookRegistrations",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketRebookRegistrations_ApplicantId",
                table: "TicketRebookRegistrations",
                newName: "IX_TicketRebookRegistrations_ApplicantProcessId");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "TicketReadies",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketReadies_ApplicantId",
                table: "TicketReadies",
                newName: "IX_TicketReadies_ApplicantProcessId");

            migrationBuilder.RenameColumn(
                name: "ApplicantId",
                table: "TraveledApplicant",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_TraveledApplicants_ApplicantId",
                table: "TraveledApplicant",
                newName: "IX_TraveledApplicant_ApplicantProcessId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 14, 13, 9, 57, 497, DateTimeKind.Local).AddTicks(9022),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 16, 16, 22, 7, 6, DateTimeKind.Local).AddTicks(215));

            migrationBuilder.AddColumn<Guid>(
                name: "TicketReadyId",
                table: "Applicants",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketRebookId",
                table: "Applicants",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketRebookRegistrationId",
                table: "Applicants",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketRefundId",
                table: "Applicants",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketRegistrationId",
                table: "Applicants",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TraveledApplicant",
                table: "TraveledApplicant",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 13, 9, 57, 504, DateTimeKind.Local).AddTicks(7971));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 13, 9, 57, 504, DateTimeKind.Local).AddTicks(7984));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 13, 9, 57, 504, DateTimeKind.Local).AddTicks(7990));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 13, 9, 57, 504, DateTimeKind.Local).AddTicks(7996));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 13, 9, 57, 504, DateTimeKind.Local).AddTicks(8015));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 13, 9, 57, 504, DateTimeKind.Local).AddTicks(8023));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 13, 9, 57, 504, DateTimeKind.Local).AddTicks(7720));

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_TicketReadyId",
                table: "Applicants",
                column: "TicketReadyId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_TicketRebookId",
                table: "Applicants",
                column: "TicketRebookId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_TicketRebookRegistrationId",
                table: "Applicants",
                column: "TicketRebookRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_TicketRefundId",
                table: "Applicants",
                column: "TicketRefundId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_TicketRegistrationId",
                table: "Applicants",
                column: "TicketRegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_TicketReadies_TicketReadyId",
                table: "Applicants",
                column: "TicketReadyId",
                principalTable: "TicketReadies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_TicketRebookRegistrations_TicketRebookRegistratio~",
                table: "Applicants",
                column: "TicketRebookRegistrationId",
                principalTable: "TicketRebookRegistrations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_TicketRebooks_TicketRebookId",
                table: "Applicants",
                column: "TicketRebookId",
                principalTable: "TicketRebooks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_TicketRefunds_TicketRefundId",
                table: "Applicants",
                column: "TicketRefundId",
                principalTable: "TicketRefunds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_TicketRegistrations_TicketRegistrationId",
                table: "Applicants",
                column: "TicketRegistrationId",
                principalTable: "TicketRegistrations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketReadies_ApplicantProcesses_ApplicantProcessId",
                table: "TicketReadies",
                column: "ApplicantProcessId",
                principalTable: "ApplicantProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRebookRegistrations_ApplicantProcesses_ApplicantProces~",
                table: "TicketRebookRegistrations",
                column: "ApplicantProcessId",
                principalTable: "ApplicantProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRebooks_ApplicantProcesses_ApplicantProcessId",
                table: "TicketRebooks",
                column: "ApplicantProcessId",
                principalTable: "ApplicantProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRefunds_ApplicantProcesses_ApplicantProcessId",
                table: "TicketRefunds",
                column: "ApplicantProcessId",
                principalTable: "ApplicantProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketRegistrations_ApplicantProcesses_ApplicantProcessId",
                table: "TicketRegistrations",
                column: "ApplicantProcessId",
                principalTable: "ApplicantProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TraveledApplicant_ApplicantProcesses_ApplicantProcessId",
                table: "TraveledApplicant",
                column: "ApplicantProcessId",
                principalTable: "ApplicantProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
