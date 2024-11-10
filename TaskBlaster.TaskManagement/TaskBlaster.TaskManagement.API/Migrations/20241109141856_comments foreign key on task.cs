using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBlaster.TaskManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class commentsforeignkeyontask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskNotifications_TaskId",
                table: "TaskNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_TaskNotifications_TaskId",
                table: "TaskNotifications",
                column: "TaskId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaskNotifications_TaskId",
                table: "TaskNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_TaskNotifications_TaskId",
                table: "TaskNotifications",
                column: "TaskId");
        }
    }
}
