using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsBasket.Api.Migrations
{
    public partial class AddUserProfilePicutre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "UserProfiles",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "UserProfiles");
        }
    }
}
