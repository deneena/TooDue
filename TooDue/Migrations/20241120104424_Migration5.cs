using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDue.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Project_User_Tasks",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "Task_id",
                table: "Project_User_Tasks",
                newName: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Project_id",
                table: "Project_User_Tasks",
                newName: "Projects");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Project_User_Roles",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "Project_id",
                table: "Project_User_Roles",
                newName: "Projects");

            migrationBuilder.RenameColumn(
                name: "Comment_user_id",
                table: "Comments",
                newName: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Project_User_Tasks_Projects",
                table: "Project_User_Tasks",
                column: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_Project_User_Tasks_Tasks",
                table: "Project_User_Tasks",
                column: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Project_User_Tasks_Users",
                table: "Project_User_Tasks",
                column: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Project_User_Roles_Projects",
                table: "Project_User_Roles",
                column: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_Project_User_Roles_Users",
                table: "Project_User_Roles",
                column: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Users",
                table: "Comments",
                column: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_Users",
                table: "Comments",
                column: "Users",
                principalTable: "Users",
                principalColumn: "User_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_Roles_Projects_Projects",
                table: "Project_User_Roles",
                column: "Projects",
                principalTable: "Projects",
                principalColumn: "Project_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_Roles_Users_Users",
                table: "Project_User_Roles",
                column: "Users",
                principalTable: "Users",
                principalColumn: "User_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_Tasks_Projects_Projects",
                table: "Project_User_Tasks",
                column: "Projects",
                principalTable: "Projects",
                principalColumn: "Project_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_Tasks_Tasks_Tasks",
                table: "Project_User_Tasks",
                column: "Tasks",
                principalTable: "Tasks",
                principalColumn: "Task_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_Tasks_Users_Users",
                table: "Project_User_Tasks",
                column: "Users",
                principalTable: "Users",
                principalColumn: "User_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_Users",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_Roles_Projects_Projects",
                table: "Project_User_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_Roles_Users_Users",
                table: "Project_User_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_Tasks_Projects_Projects",
                table: "Project_User_Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_Tasks_Tasks_Tasks",
                table: "Project_User_Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_Tasks_Users_Users",
                table: "Project_User_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Project_User_Tasks_Projects",
                table: "Project_User_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Project_User_Tasks_Tasks",
                table: "Project_User_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Project_User_Tasks_Users",
                table: "Project_User_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Project_User_Roles_Projects",
                table: "Project_User_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Project_User_Roles_Users",
                table: "Project_User_Roles");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Users",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Users",
                table: "Project_User_Tasks",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "Tasks",
                table: "Project_User_Tasks",
                newName: "Task_id");

            migrationBuilder.RenameColumn(
                name: "Projects",
                table: "Project_User_Tasks",
                newName: "Project_id");

            migrationBuilder.RenameColumn(
                name: "Users",
                table: "Project_User_Roles",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "Projects",
                table: "Project_User_Roles",
                newName: "Project_id");

            migrationBuilder.RenameColumn(
                name: "Users",
                table: "Comments",
                newName: "Comment_user_id");
        }
    }
}
