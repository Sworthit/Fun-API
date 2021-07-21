using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FunApi.Migrations
{
    public partial class DataSeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "GeneratedDate",
                table: "GeneratedName",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Names",
                columns: new[] { "Id", "name" },
                values: new object[] { 1, "Mat" });

            migrationBuilder.InsertData(
                table: "Names",
                columns: new[] { "Id", "name" },
                values: new object[] { 2, "Maciek" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Names",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Names",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "GeneratedDate",
                table: "GeneratedName");
        }
    }
}
