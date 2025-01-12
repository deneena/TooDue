using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDue.Migrations
{
    /// <inheritdoc />
    public partial class fkTaskProj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_Project_id",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Project_id",
                table: "Tasks",
                newName: "Project_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Project_id",
                table: "Tasks",
                newName: "IX_Tasks_Project_Id");

            migrationBuilder.AlterColumn<int>(
                name: "Project_Id",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_Project_Id",
                table: "Tasks",
                column: "Project_Id",
                principalTable: "Projects",
                principalColumn: "Project_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_Project_Id",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Project_Id",
                table: "Tasks",
                newName: "Project_id");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Project_Id",
                table: "Tasks",
                newName: "IX_Tasks_Project_id");

            migrationBuilder.AlterColumn<int>(
                name: "Project_id",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_Project_id",
                table: "Tasks",
                column: "Project_id",
                principalTable: "Projects",
                principalColumn: "Project_id");
        }
    }
}
