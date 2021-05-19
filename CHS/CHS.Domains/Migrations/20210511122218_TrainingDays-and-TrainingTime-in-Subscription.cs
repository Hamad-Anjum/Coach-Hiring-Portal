
using Microsoft.EntityFrameworkCore.Migrations;

namespace CHS.Domains.Migrations
{
    public partial class TrainingDaysandTrainingTimeinSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrainingDays",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingTiming",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrainingDays",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "TrainingTiming",
                table: "Subscriptions");
        }
    }
}
