using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class AfterMergingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_LookUps_AddressRegionId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "TicketRequired",
                table: "Processes");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "DistrictArabic",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "StreetArabic",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "SubCityArabic",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "WoredaArabic",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AddressRegionId",
                table: "Addresses",
                newName: "RegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AddressRegionId",
                table: "Addresses",
                newName: "IX_Addresses_RegionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 15, 10, 39, 12, 804, DateTimeKind.Local).AddTicks(476),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 14, 16, 28, 26, 718, DateTimeKind.Local).AddTicks(9487));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProcessDefinitions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Addresses",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "TicketReadies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateInterval = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TicketOfficeId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApplicantProcessId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketReadies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketReadies_ApplicantProcesses_ApplicantProcessId",
                        column: x => x.ApplicantProcessId,
                        principalTable: "ApplicantProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketReadies_LookUps_TicketOfficeId",
                        column: x => x.TicketOfficeId,
                        principalTable: "LookUps",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TicketRebookRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TicketNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlightDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureTime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Transit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArrivalTime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TicketPrice = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AirLineId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApplicantProcessId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRebookRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRebookRegistrations_ApplicantProcesses_ApplicantProces~",
                        column: x => x.ApplicantProcessId,
                        principalTable: "ApplicantProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketRebookRegistrations_LookUps_AirLineId",
                        column: x => x.AirLineId,
                        principalTable: "LookUps",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TicketRebooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateInterval = table.Column<int>(type: "int", nullable: false),
                    ApplicantProcessId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRebooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRebooks_ApplicantProcesses_ApplicantProcessId",
                        column: x => x.ApplicantProcessId,
                        principalTable: "ApplicantProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TicketRefunds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateInterval = table.Column<int>(type: "int", nullable: false),
                    ApplicantProcessId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRefunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRefunds_ApplicantProcesses_ApplicantProcessId",
                        column: x => x.ApplicantProcessId,
                        principalTable: "ApplicantProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TicketRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TicketNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlightDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DepartureTime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Transit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArrivalTime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remark = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TicketPrice = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AirLineId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApplicantProcessId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRegistrations_ApplicantProcesses_ApplicantProcessId",
                        column: x => x.ApplicantProcessId,
                        principalTable: "ApplicantProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketRegistrations_LookUps_AirLineId",
                        column: x => x.AirLineId,
                        principalTable: "LookUps",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.InsertData(
                table: "Processes",
                columns: new[] { "Id", "CountryId", "CreatedAt", "CreatedBy", "EnjazRequired", "ModifiedAt", "ModifiedBy", "Name", "Step", "VisaRequired" },
                values: new object[] { new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), null, new DateTime(2023, 6, 15, 10, 39, 12, 814, DateTimeKind.Local).AddTicks(7982), null, false, null, null, "Ticket Process", 100, true });

            migrationBuilder.InsertData(
                table: "ProcessDefinitions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "Name", "ProcessId", "RequestApproval", "Step" },
                values: new object[,]
                {
                    { new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"), new DateTime(2023, 6, 15, 10, 39, 12, 814, DateTimeKind.Local).AddTicks(8316), null, null, null, "Ready to Issue Ticket", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), false, 0 },
                    { new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"), new DateTime(2023, 6, 15, 10, 39, 12, 814, DateTimeKind.Local).AddTicks(8336), null, null, null, "Register Ticket", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), false, 1 },
                    { new Guid("2d9ef769-6d03-4406-9849-430ff9723778"), new DateTime(2023, 6, 15, 10, 39, 12, 814, DateTimeKind.Local).AddTicks(8343), null, null, null, "Refund Ticket", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), false, 2 },
                    { new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"), new DateTime(2023, 6, 15, 10, 39, 12, 814, DateTimeKind.Local).AddTicks(8349), null, null, null, "Rebook Ticket", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), false, 3 },
                    { new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"), new DateTime(2023, 6, 15, 10, 39, 12, 814, DateTimeKind.Local).AddTicks(8354), null, null, null, "Register Rebook Ticket", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), false, 3 },
                    { new Guid("5b912c00-9df3-47a1-a525-410abf239616"), new DateTime(2023, 6, 15, 10, 39, 12, 814, DateTimeKind.Local).AddTicks(8362), null, null, null, "Travel", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), true, 2 }
                });

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
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReadies_ApplicantProcessId",
                table: "TicketReadies",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketReadies_TicketOfficeId",
                table: "TicketReadies",
                column: "TicketOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebookRegistrations_AirLineId",
                table: "TicketRebookRegistrations",
                column: "AirLineId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebookRegistrations_ApplicantProcessId",
                table: "TicketRebookRegistrations",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebooks_ApplicantProcessId",
                table: "TicketRebooks",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRefunds_ApplicantProcessId",
                table: "TicketRefunds",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRegistrations_AirLineId",
                table: "TicketRegistrations",
                column: "AirLineId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRegistrations_ApplicantProcessId",
                table: "TicketRegistrations",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TraveledApplicant_ApplicantProcessId",
                table: "TraveledApplicant",
                column: "ApplicantProcessId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_LookUps_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_LookUps_RegionId",
                table: "Addresses",
                column: "RegionId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_LookUps_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_LookUps_RegionId",
                table: "Addresses");

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

            migrationBuilder.DropTable(
                name: "TicketReadies");

            migrationBuilder.DropTable(
                name: "TicketRebookRegistrations");

            migrationBuilder.DropTable(
                name: "TicketRebooks");

            migrationBuilder.DropTable(
                name: "TicketRefunds");

            migrationBuilder.DropTable(
                name: "TicketRegistrations");

            migrationBuilder.DropTable(
                name: "TraveledApplicant");

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

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses");

            migrationBuilder.DeleteData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"));

            migrationBuilder.DeleteData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"));

            migrationBuilder.DeleteData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("2d9ef769-6d03-4406-9849-430ff9723778"));

            migrationBuilder.DeleteData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"));

            migrationBuilder.DeleteData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("4048b353-039d-41b6-8690-a9aaa2e679cf"));

            migrationBuilder.DeleteData(
                table: "ProcessDefinitions",
                keyColumn: "Id",
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"));

            migrationBuilder.DeleteData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"));

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

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Addresses",
                newName: "AddressRegionId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_RegionId",
                table: "Addresses",
                newName: "IX_Addresses_AddressRegionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 14, 16, 28, 26, 718, DateTimeKind.Local).AddTicks(9487),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 15, 10, 39, 12, 804, DateTimeKind.Local).AddTicks(476));

            migrationBuilder.AddColumn<bool>(
                name: "TicketRequired",
                table: "Processes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ProcessDefinitions",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProcessDefinitions",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "City",
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
                name: "Region",
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

            migrationBuilder.AddColumn<string>(
                name: "SubCityArabic",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "WoredaArabic",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_LookUps_AddressRegionId",
                table: "Addresses",
                column: "AddressRegionId",
                principalTable: "LookUps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
