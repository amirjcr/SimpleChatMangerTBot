using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatMangementApi.Migrations
{
    public partial class GenerateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberCount = table.Column<int>(type: "int", nullable: false),
                    GroupAdded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminGroups",
                columns: table => new
                {
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminGroups", x => new { x.AdminId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_AdminGroups_Admins_AdminId",
                        column: x => x.AdminId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BotSettings",
                columns: table => new
                {
                    SettingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LockGap = table.Column<bool>(type: "bit", nullable: false),
                    LockVoice = table.Column<bool>(type: "bit", nullable: false),
                    LockVideo = table.Column<bool>(type: "bit", nullable: false),
                    LockSticker = table.Column<bool>(type: "bit", nullable: false),
                    Group_Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotSettings", x => x.SettingId);
                    table.ForeignKey(
                        name: "FK_BotSettings_Groups_Group_Id",
                        column: x => x.Group_Id,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LimitedPeoples",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mute = table.Column<bool>(type: "bit", nullable: false),
                    stratDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    enddate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Group_Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimitedPeoples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LimitedPeoples_Groups_Group_Id",
                        column: x => x.Group_Id,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminGroups_GroupId",
                table: "AdminGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BotSettings_Group_Id",
                table: "BotSettings",
                column: "Group_Id",
                unique: true,
                filter: "[Group_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LimitedPeoples_Group_Id",
                table: "LimitedPeoples",
                column: "Group_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminGroups");

            migrationBuilder.DropTable(
                name: "BotSettings");

            migrationBuilder.DropTable(
                name: "LimitedPeoples");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
