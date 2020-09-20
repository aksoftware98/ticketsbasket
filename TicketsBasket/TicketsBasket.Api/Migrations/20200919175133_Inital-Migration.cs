using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsBasket.Api.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 25, nullable: false),
                    LastName = table.Column<string>(maxLength: 25, nullable: false),
                    Email = table.Column<string>(maxLength: 25, nullable: false),
                    Country = table.Column<string>(maxLength: 25, nullable: false),
                    City = table.Column<string>(maxLength: 25, nullable: false),
                    IsOrganizer = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(maxLength: 80, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    CoverImageUrl = table.Column<string>(maxLength: 256, nullable: false),
                    Location = table.Column<string>(maxLength: 256, nullable: false),
                    TicketsCount = table.Column<int>(nullable: false),
                    Views = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UserProfileId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CvUrl = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AppliedUserId = table.Column<string>(nullable: true),
                    OrganizerId = table.Column<string>(nullable: true),
                    ApplicationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_UserProfiles_AppliedUserId",
                        column: x => x.AppliedUserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobApplications_UserProfiles_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventImages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 256, nullable: false),
                    ThumpUrl = table.Column<string>(maxLength: 256, nullable: true),
                    EventId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventImages_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventTags",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    EventId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTags_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    LikedOn = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<string>(nullable: true),
                    UserProfileId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Barcode = table.Column<string>(maxLength: 10, nullable: false),
                    Discount = table.Column<int>(nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Place = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<string>(nullable: true),
                    UserProfileId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WishlistEvents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    EventId = table.Column<string>(nullable: true),
                    UserProfileId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WishlistEvents_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventImages_EventId",
                table: "EventImages",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserProfileId",
                table: "Events",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTags_EventId",
                table: "EventTags",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_AppliedUserId",
                table: "JobApplications",
                column: "AppliedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_OrganizerId",
                table: "JobApplications",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_EventId",
                table: "Likes",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserProfileId",
                table: "Likes",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserProfileId",
                table: "Tickets",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistEvents_EventId",
                table: "WishlistEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistEvents_UserProfileId",
                table: "WishlistEvents",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventImages");

            migrationBuilder.DropTable(
                name: "EventTags");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "WishlistEvents");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
