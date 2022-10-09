using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations
{
    public partial class AddCesar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cesars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShiftAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    PlainText = table.Column<string>(type: "TEXT", nullable: false),
                    CipherText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cesars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cesars");
        }
    }
}
