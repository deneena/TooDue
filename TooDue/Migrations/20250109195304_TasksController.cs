using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooDue.Migrations
{
    /// <inheritdoc />
    public partial class TasksController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Task_deadline",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Task_label",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Task_update_date",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Task_status",
                table: "Tasks",
                newName: "Link_to_media");

            migrationBuilder.RenameColumn(
                name: "Is_collaborative",
                table: "Tasks",
                newName: "Task_completion");

            migrationBuilder.CreateTable(
                name: "ProjectUserTask",
                columns: table => new
                {
                    Put_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Project_id = table.Column<int>(type: "int", nullable: false),
                    User_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Task_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUserTask", x => x.Put_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUserTask");

            migrationBuilder.RenameColumn(
                name: "Task_completion",
                table: "Tasks",
                newName: "Is_collaborative");

            migrationBuilder.RenameColumn(
                name: "Link_to_media",
                table: "Tasks",
                newName: "Task_status");

            migrationBuilder.AddColumn<DateTime>(
                name: "Task_deadline",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Task_label",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Task_update_date",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
