using Microsoft.EntityFrameworkCore.Migrations;

namespace CHS.Domains.Migrations
{
    public partial class MainSiteImageinApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainSiteImage",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainSiteImage",
                table: "AspNetUsers");
        }
    }
}
