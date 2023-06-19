using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class Mod4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketReadies_Applicants_ApplicantId",
                table: "TicketReadies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketReadies_ProcessDefinitions_ProcessDefinitionId",
                table: "TicketReadies");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRebookRegistrations_Applicants_ApplicantId",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRebookRegistrations_ProcessDefinitions_ProcessDefiniti~",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRebooks_Applicants_ApplicantId",
                table: "TicketRebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRebooks_ProcessDefinitions_ProcessDefinitionId",
                table: "TicketRebooks");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRefunds_Applicants_ApplicantId",
                table: "TicketRefunds");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRefunds_ProcessDefinitions_ProcessDefinitionId",
                table: "TicketRefunds");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRegistrations_Applicants_ApplicantId",
                table: "TicketRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketRegistrations_ProcessDefinitions_ProcessDefinitionId",
                table: "TicketRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TicketRegistrations_ApplicantId",
                table: "TicketRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TicketRegistrations_ProcessDefinitionId",
                table: "TicketRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TicketRefunds_ApplicantId",
                table: "TicketRefunds");

            migrationBuilder.DropIndex(
                name: "IX_TicketRefunds_ProcessDefinitionId",
                table: "TicketRefunds");

            migrationBuilder.DropIndex(
                name: "IX_TicketRebooks_ApplicantId",
                table: "TicketRebooks");

            migrationBuilder.DropIndex(
                name: "IX_TicketRebooks_ProcessDefinitionId",
                table: "TicketRebooks");

            migrationBuilder.DropIndex(
                name: "IX_TicketRebookRegistrations_ApplicantId",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TicketRebookRegistrations_ProcessDefinitionId",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TicketReadies_ApplicantId",
                table: "TicketReadies");

            migrationBuilder.DropIndex(
                name: "IX_TicketReadies_ProcessDefinitionId",
                table: "TicketReadies");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "TicketRegistrations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TicketRegistrations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TicketRegistrations");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "TicketRefunds");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TicketRefunds");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TicketRefunds");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "TicketRebooks");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TicketRebooks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TicketRebooks");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "TicketReadies");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TicketReadies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TicketReadies");

            migrationBuilder.RenameColumn(
                name: "ProcessDefinitionId",
                table: "TicketRegistrations",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameColumn(
                name: "ProcessDefinitionId",
                table: "TicketRefunds",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameColumn(
                name: "ProcessDefinitionId",
                table: "TicketRebooks",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameColumn(
                name: "ProcessDefinitionId",
                table: "TicketRebookRegistrations",
                newName: "ApplicantProcessId");

            migrationBuilder.RenameColumn(
                name: "ProcessDefinitionId",
                table: "TicketReadies",
                newName: "ApplicantProcessId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 14, 10, 43, 11, 195, DateTimeKind.Local).AddTicks(4961),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 13, 15, 8, 53, 275, DateTimeKind.Local).AddTicks(7546));

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

            migrationBuilder.CreateTable(
                name: "TraveledApplicant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Remark = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApplicantProcessId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraveledApplicant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraveledApplicant_ApplicantProcesses_ApplicantProcessId",
                        column: x => x.ApplicantProcessId,
                        principalTable: "ApplicantProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 10, 43, 11, 202, DateTimeKind.Local).AddTicks(1223));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 10, 43, 11, 202, DateTimeKind.Local).AddTicks(1244));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 10, 43, 11, 202, DateTimeKind.Local).AddTicks(1252));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 10, 43, 11, 202, DateTimeKind.Local).AddTicks(1261));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 10, 43, 11, 202, DateTimeKind.Local).AddTicks(1268));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 10, 43, 11, 202, DateTimeKind.Local).AddTicks(1293));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 14, 10, 43, 11, 202, DateTimeKind.Local).AddTicks(938));

            migrationBuilder.CreateIndex(
                name: "IX_TicketRegistrations_ApplicantProcessId",
                table: "TicketRegistrations",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRefunds_ApplicantProcessId",
                table: "TicketRefunds",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebooks_ApplicantProcessId",
                table: "TicketRebooks",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebookRegistrations_ApplicantProcessId",
                table: "TicketRebookRegistrations",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketReadies_ApplicantProcessId",
                table: "TicketReadies",
                column: "ApplicantProcessId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_TraveledApplicant_ApplicantProcessId",
                table: "TraveledApplicant",
                column: "ApplicantProcessId",
                unique: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "TraveledApplicant");

            migrationBuilder.DropIndex(
                name: "IX_TicketRegistrations_ApplicantProcessId",
                table: "TicketRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TicketRefunds_ApplicantProcessId",
                table: "TicketRefunds");

            migrationBuilder.DropIndex(
                name: "IX_TicketRebooks_ApplicantProcessId",
                table: "TicketRebooks");

            migrationBuilder.DropIndex(
                name: "IX_TicketRebookRegistrations_ApplicantProcessId",
                table: "TicketRebookRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_TicketReadies_ApplicantProcessId",
                table: "TicketReadies");

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

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketRegistrations",
                newName: "ProcessDefinitionId");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketRefunds",
                newName: "ProcessDefinitionId");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketRebooks",
                newName: "ProcessDefinitionId");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketRebookRegistrations",
                newName: "ProcessDefinitionId");

            migrationBuilder.RenameColumn(
                name: "ApplicantProcessId",
                table: "TicketReadies",
                newName: "ProcessDefinitionId");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "TicketRegistrations",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TicketRegistrations",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TicketRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "TicketRefunds",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TicketRefunds",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TicketRefunds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "TicketRebooks",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TicketRebooks",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TicketRebooks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "TicketRebookRegistrations",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TicketRebookRegistrations",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TicketRebookRegistrations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "TicketReadies",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TicketReadies",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TicketReadies",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 13, 15, 8, 53, 275, DateTimeKind.Local).AddTicks(7546),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 14, 10, 43, 11, 195, DateTimeKind.Local).AddTicks(4961));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 13, 15, 8, 53, 282, DateTimeKind.Local).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 13, 15, 8, 53, 282, DateTimeKind.Local).AddTicks(2159));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 13, 15, 8, 53, 282, DateTimeKind.Local).AddTicks(2165));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 13, 15, 8, 53, 282, DateTimeKind.Local).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 13, 15, 8, 53, 282, DateTimeKind.Local).AddTicks(2175));

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 13, 15, 8, 53, 282, DateTimeKind.Local).AddTicks(2198));

            migrationBuilder.UpdateData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"),
                column: "CreatedAt",
                value: new DateTime(2023, 6, 13, 15, 8, 53, 282, DateTimeKind.Local).AddTicks(1934));

            migrationBuilder.CreateIndex(
                name: "IX_TicketRegistrations_ApplicantId",
                table: "TicketRegistrations",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRegistrations_ProcessDefinitionId",
                table: "TicketRegistrations",
                column: "ProcessDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRefunds_ApplicantId",
                table: "TicketRefunds",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRefunds_ProcessDefinitionId",
                table: "TicketRefunds",
                column: "ProcessDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebooks_ApplicantId",
                table: "TicketRebooks",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebooks_ProcessDefinitionId",
                table: "TicketRebooks",
                column: "ProcessDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebookRegistrations_ApplicantId",
                table: "TicketRebookRegistrations",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebookRegistrations_ProcessDefinitionId",
                table: "TicketRebookRegistrations",
                column: "ProcessDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReadies_ApplicantId",
                table: "TicketReadies",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketReadies_ProcessDefinitionId",
                table: "TicketReadies",
                column: "ProcessDefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketReadies_Applicants_ApplicantId",
                table: "TicketReadies",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketReadies_ProcessDefinitions_ProcessDefinitionId",
                table: "TicketReadies",
                column: "ProcessDefinitionId",
                principalTable: "ProcessDefinitions",
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
                name: "FK_TicketRebookRegistrations_ProcessDefinitions_ProcessDefiniti~",
                table: "TicketRebookRegistrations",
                column: "ProcessDefinitionId",
                principalTable: "ProcessDefinitions",
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
                name: "FK_TicketRebooks_ProcessDefinitions_ProcessDefinitionId",
                table: "TicketRebooks",
                column: "ProcessDefinitionId",
                principalTable: "ProcessDefinitions",
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
                name: "FK_TicketRefunds_ProcessDefinitions_ProcessDefinitionId",
                table: "TicketRefunds",
                column: "ProcessDefinitionId",
                principalTable: "ProcessDefinitions",
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
                name: "FK_TicketRegistrations_ProcessDefinitions_ProcessDefinitionId",
                table: "TicketRegistrations",
                column: "ProcessDefinitionId",
                principalTable: "ProcessDefinitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
