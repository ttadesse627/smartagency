using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class TicketProcessModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketRequired",
                table: "Processes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 13, 8, 28, 28, 415, DateTimeKind.Local).AddTicks(1395),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 11, 22, 1, 27, 506, DateTimeKind.Local).AddTicks(2329));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProcessDefinitions",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TicketReadies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateInterval = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    TicketOfficeId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ProcessDefinitionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApplicantId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketReadies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketReadies_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketReadies_LookUps_TicketOfficeId",
                        column: x => x.TicketOfficeId,
                        principalTable: "LookUps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketReadies_ProcessDefinitions_ProcessDefinitionId",
                        column: x => x.ProcessDefinitionId,
                        principalTable: "ProcessDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    AirLineId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ProcessDefinitionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApplicantId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRebookRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRebookRegistrations_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketRebookRegistrations_LookUps_AirLineId",
                        column: x => x.AirLineId,
                        principalTable: "LookUps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketRebookRegistrations_ProcessDefinitions_ProcessDefiniti~",
                        column: x => x.ProcessDefinitionId,
                        principalTable: "ProcessDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TicketRebooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateInterval = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    ProcessDefinitionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApplicantId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRebooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRebooks_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketRebooks_ProcessDefinitions_ProcessDefinitionId",
                        column: x => x.ProcessDefinitionId,
                        principalTable: "ProcessDefinitions",
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
                    ProcessDefinitionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApplicantId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRefunds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRefunds_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketRefunds_ProcessDefinitions_ProcessDefinitionId",
                        column: x => x.ProcessDefinitionId,
                        principalTable: "ProcessDefinitions",
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
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    AirLineId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ProcessDefinitionId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ApplicantId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketRegistrations_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketRegistrations_LookUps_AirLineId",
                        column: x => x.AirLineId,
                        principalTable: "LookUps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TicketRegistrations_ProcessDefinitions_ProcessDefinitionId",
                        column: x => x.ProcessDefinitionId,
                        principalTable: "ProcessDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Processes",
                columns: new[] { "Id", "CountryId", "CreatedAt", "CreatedBy", "EnjazRequired", "ModifiedAt", "ModifiedBy", "Name", "Step", "VisaRequired" },
                values: new object[] { new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), null, new DateTime(2023, 6, 13, 8, 28, 28, 422, DateTimeKind.Local).AddTicks(8758), null, false, null, null, "Ticket Process", 100, true });

            migrationBuilder.InsertData(
                table: "ProcessDefinitions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy", "Name", "ProcessId", "RequestApproval", "Step" },
                values: new object[,]
                {
                    { new Guid("00fa1a8e-ac70-400e-8f37-20010f81a27a"), new DateTime(2023, 6, 13, 8, 28, 28, 422, DateTimeKind.Local).AddTicks(9139), null, null, null, "Ready to Issue Ticket", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), false, 0 },
                    { new Guid("1dc479ab-fe84-4ca8-828f-9a21de7434e7"), new DateTime(2023, 6, 13, 8, 28, 28, 422, DateTimeKind.Local).AddTicks(9162), null, null, null, "Register Ticket", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), false, 1 },
                    { new Guid("2d9ef769-6d03-4406-9849-430ff9723778"), new DateTime(2023, 6, 13, 8, 28, 28, 422, DateTimeKind.Local).AddTicks(9172), null, null, null, "Refund Ticket", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), false, 2 },
                    { new Guid("3048b353-039d-41b6-8690-a9aaa2e679cf"), new DateTime(2023, 6, 13, 8, 28, 28, 422, DateTimeKind.Local).AddTicks(9182), null, null, null, "Rebook Ticket", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), false, 3 },
                    { new Guid("5b912c00-9df3-47a1-a525-410abf239616"), new DateTime(2023, 6, 13, 8, 28, 28, 422, DateTimeKind.Local).AddTicks(9193), null, null, null, "Travel", new Guid("60209c9d-47b4-497b-8abd-94a753814a86"), true, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketReadies_ApplicantId",
                table: "TicketReadies",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketReadies_ProcessDefinitionId",
                table: "TicketReadies",
                column: "ProcessDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketReadies_TicketOfficeId",
                table: "TicketReadies",
                column: "TicketOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebookRegistrations_AirLineId",
                table: "TicketRebookRegistrations",
                column: "AirLineId");

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
                name: "IX_TicketRebooks_ApplicantId",
                table: "TicketRebooks",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRebooks_ProcessDefinitionId",
                table: "TicketRebooks",
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
                name: "IX_TicketRegistrations_AirLineId",
                table: "TicketRegistrations",
                column: "AirLineId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketRegistrations_ApplicantId",
                table: "TicketRegistrations",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketRegistrations_ProcessDefinitionId",
                table: "TicketRegistrations",
                column: "ProcessDefinitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: new Guid("5b912c00-9df3-47a1-a525-410abf239616"));

            migrationBuilder.DeleteData(
                table: "Processes",
                keyColumn: "Id",
                keyValue: new Guid("60209c9d-47b4-497b-8abd-94a753814a86"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 11, 22, 1, 27, 506, DateTimeKind.Local).AddTicks(2329),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 6, 13, 8, 28, 28, 415, DateTimeKind.Local).AddTicks(1395));

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
        }
    }
}
