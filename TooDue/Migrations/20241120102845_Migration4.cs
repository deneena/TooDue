using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDue.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Themes_Theme_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Theme_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Theme_id",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "User_selected_theme",
                table: "Users",
                newName: "Themes");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Themes",
                table: "Users",
                column: "Themes");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Themes_Themes",
                table: "Users",
                column: "Themes",
                principalTable: "Themes",
                principalColumn: "Theme_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Themes_Themes",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Themes",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Themes",
                table: "Users",
                newName: "User_selected_theme");

            migrationBuilder.AddColumn<int>(
                name: "Theme_id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Theme_id",
                table: "Users",
                column: "Theme_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Themes_Theme_id",
                table: "Users",
                column: "Theme_id",
                principalTable: "Themes",
                principalColumn: "Theme_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
