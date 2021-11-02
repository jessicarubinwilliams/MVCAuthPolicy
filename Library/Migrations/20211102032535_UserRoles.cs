using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (1,'Librarian','LIBRARIAN')");
        migrationBuilder.Sql($"INSERT INTO AspNetRoles (Id, Name, NormalizedName) VALUES (2,'Patron','PATRON')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = 1");
        migrationBuilder.Sql($"DELETE FROM AspNetRoles WHERE Id = 2");
        }
    }
}