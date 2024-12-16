using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDue.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
