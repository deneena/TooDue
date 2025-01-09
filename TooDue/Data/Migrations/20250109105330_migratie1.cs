using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDue.Data.Migrations
{
    /// <inheritdoc />
    public partial class migratie1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project_create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Project_id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Task_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Task_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is_collaborative = table.Column<bool>(type: "bit", nullable: false),
                    Task_deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Task_create_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Task_update_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Task_complete_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Task_id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Theme_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Main_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Light_accent_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dark_accent_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transparent_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Animal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flower = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Background_repeat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.Theme_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_displayname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profile_picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Banner_picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Themes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_id);
                    table.ForeignKey(
                        name: "FK_User_Theme_Themes",
                        column: x => x.Themes,
                        principalTable: "Theme",
                        principalColumn: "Theme_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Users = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Comment_id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_User_Users",
                        column: x => x.Users,
                        principalTable: "User",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ApplicationUserId",
                table: "Comments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Users",
                table: "Comments",
                column: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ApplicationUserId",
                table: "Projects",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ApplicationUserId",
                table: "Tasks",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Themes",
                table: "User",
                column: "Themes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
