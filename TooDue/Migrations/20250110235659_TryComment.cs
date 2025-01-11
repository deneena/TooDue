using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDue.Migrations
{
    /// <inheritdoc />
    public partial class TryComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_User_Users",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Users",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Users",
                table: "Comments",
                newName: "TaskId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Comments",
                newName: "Users");

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Theme_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Animal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Background_repeat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dark_accent_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flower = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Light_accent_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Main_color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transparent_color = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Themes = table.Column<int>(type: "int", nullable: false),
                    Banner_picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profile_picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_displayname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Users",
                table: "Comments",
                column: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_User_Themes",
                table: "User",
                column: "Themes");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_User_Users",
                table: "Comments",
                column: "Users",
                principalTable: "User",
                principalColumn: "User_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
