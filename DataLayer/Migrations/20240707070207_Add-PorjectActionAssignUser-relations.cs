using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPorjectActionAssignUserrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "ProjectAction",
                newName: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActionAssignUsers_ProjectActionId",
                table: "ProjectActionAssignUsers",
                column: "ProjectActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActionAssignUsers_UserAssignedId",
                table: "ProjectActionAssignUsers",
                column: "UserAssignedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActionAssignUsers_UserRoleId",
                table: "ProjectActionAssignUsers",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActionAssignUsers_ProjectAction_ProjectActionId",
                table: "ProjectActionAssignUsers",
                column: "ProjectActionId",
                principalTable: "ProjectAction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActionAssignUsers_UserRole_UserRoleId",
                table: "ProjectActionAssignUsers",
                column: "UserRoleId",
                principalTable: "UserRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActionAssignUsers_User_UserAssignedId",
                table: "ProjectActionAssignUsers",
                column: "UserAssignedId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActionAssignUsers_ProjectAction_ProjectActionId",
                table: "ProjectActionAssignUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActionAssignUsers_UserRole_UserRoleId",
                table: "ProjectActionAssignUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActionAssignUsers_User_UserAssignedId",
                table: "ProjectActionAssignUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActionAssignUsers_ProjectActionId",
                table: "ProjectActionAssignUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActionAssignUsers_UserAssignedId",
                table: "ProjectActionAssignUsers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActionAssignUsers_UserRoleId",
                table: "ProjectActionAssignUsers");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ProjectAction",
                newName: "Comment");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
