using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDue.Migrations
{
    /// <inheritdoc />
    public partial class icant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Project_id",
                table: "ProjectUserTask",
                newName: "ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "Project_id",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Project_id",
                table: "Tasks",
                column: "Project_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_Project_id",
                table: "Tasks",
                column: "Project_id",
                principalTable: "Projects",
                principalColumn: "Project_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_Project_id",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Project_id",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Project_id",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ProjectUserTask",
                newName: "Project_id");
        }
    }
}
