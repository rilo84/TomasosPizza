using Microsoft.EntityFrameworkCore.Migrations;

namespace TomasosPizzeria.Migrations
{
    public partial class ExtendIdentityUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KundId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KundId",
                table: "AspNetUsers");
        }
    }
}
