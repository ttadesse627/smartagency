using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class RelMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 20, 22, 50, 2, 436, DateTimeKind.Local).AddTicks(8889),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 20, 8, 48, 50, 183, DateTimeKind.Local).AddTicks(6998));

            migrationBuilder.CreateTable(
                name: "Awards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LookUpId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    EducationId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Awards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Awards_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Awards_LookUps_LookUpId",
                        column: x => x.LookUpId,
                        principalTable: "LookUps",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LevelOfQualifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LookUpId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    EducationId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelOfQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelOfQualifications_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LevelOfQualifications_LookUps_LookUpId",
                        column: x => x.LookUpId,
                        principalTable: "LookUps",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QualificationTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LookUpId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    EducationId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualificationTypes_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QualificationTypes_LookUps_LookUpId",
                        column: x => x.LookUpId,
                        principalTable: "LookUps",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ApplicantId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LookUpId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skills_LookUps_LookUpId",
                        column: x => x.LookUpId,
                        principalTable: "LookUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_EducationId",
                table: "Awards",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_LookUpId",
                table: "Awards",
                column: "LookUpId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelOfQualifications_EducationId",
                table: "LevelOfQualifications",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelOfQualifications_LookUpId",
                table: "LevelOfQualifications",
                column: "LookUpId");

            migrationBuilder.CreateIndex(
                name: "IX_QualificationTypes_EducationId",
                table: "QualificationTypes",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_QualificationTypes_LookUpId",
                table: "QualificationTypes",
                column: "LookUpId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ApplicantId",
                table: "Skills",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_LookUpId",
                table: "Skills",
                column: "LookUpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Awards");

            migrationBuilder.DropTable(
                name: "LevelOfQualifications");

            migrationBuilder.DropTable(
                name: "QualificationTypes");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 5, 20, 8, 48, 50, 183, DateTimeKind.Local).AddTicks(6998),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 5, 20, 22, 50, 2, 436, DateTimeKind.Local).AddTicks(8889));
        }
    }
}
