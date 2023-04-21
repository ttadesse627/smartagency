using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDiv.SmartAgency.Infrastructure.Migrations
{
    public partial class AddCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropColumn(
                name: "FirstNameStr",
                table: "PersonalInfo");

            migrationBuilder.RenameColumn(
                name: "MiddleNameStr",
                table: "PersonalInfo",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "LastNameStr",
                table: "PersonalInfo",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "PersonalInfo",
                newName: "FirstName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 21, 18, 28, 44, 354, DateTimeKind.Local).AddTicks(2146),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 21, 6, 21, 20, 821, DateTimeKind.Local).AddTicks(5654));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "PersonalInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "3c51d531-0314-4e3f-943f-f8c1b040cbff", "Skill" },
                    { "89a6b010-eb44-44dd-9028-fec165887693", "Language" },
                    { "b886958a-8fca-4361-b673-4ab18dbcc5c2", "Country" },
                    { "c419e08d-dec2-4496-a5aa-bf8345146563", "Qualification Type" },
                    { "ef3a922e-0e9a-40dd-b246-ff00ce7ec6e9", "Award" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PersonalInfo");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "PersonalInfo",
                newName: "MiddleNameStr");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "PersonalInfo",
                newName: "LastNameStr");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "PersonalInfo",
                newName: "GenderId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Suffixes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 21, 6, 21, 20, 821, DateTimeKind.Local).AddTicks(5654),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 4, 21, 18, 28, 44, 354, DateTimeKind.Local).AddTicks(2146));

            migrationBuilder.AddColumn<string>(
                name: "FirstNameStr",
                table: "PersonalInfo",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2023, 4, 21, 6, 21, 20, 821, DateTimeKind.Local).AddTicks(4338)),
                    CreatedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IDX_UQ_Gender",
                table: "Genders",
                column: "Name",
                unique: true);
        }
    }
}
