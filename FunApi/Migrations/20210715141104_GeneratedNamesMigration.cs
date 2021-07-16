using Microsoft.EntityFrameworkCore.Migrations;

namespace FunApi.Migrations
{
    public partial class GeneratedNamesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nameId",
                table: "Names",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "GeneratedName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedName", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratedName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Names",
                newName: "nameId");
        }
    }
}
