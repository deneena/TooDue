using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDue.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment_text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment_user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Comment_id);
                });

            migrationBuilder.CreateTable(
                name: "Project_User_Roles",
                columns: table => new
                {
                    Pur_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project_User_Roles", x => x.Pur_id);
                });

            migrationBuilder.CreateTable(
                name: "Project_User_Tasks",
                columns: table => new
                {
                    Put_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Project_id = table.Column<int>(type: "int", nullable: false),
                    Task_id = table.Column<int>(type: "int", nullable: false),
                    Task_completion = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project_User_Tasks", x => x.Put_id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Project_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project_create_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Project_id);
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
                    Task_complete_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Task_id);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Theme_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Main_color = table.Column<int>(type: "int", nullable: false),
                    Light_accent_color = table.Column<int>(type: "int", nullable: false),
                    Dark_accent_color = table.Column<int>(type: "int", nullable: false),
                    Animal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flower = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Theme_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
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
                    User_selected_theme = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Project_User_Roles");

            migrationBuilder.DropTable(
                name: "Project_User_Tasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
