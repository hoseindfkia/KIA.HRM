using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPorjectActionAssignUserentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectAction_User_UserDestinationId",
                table: "ProjectAction");

            migrationBuilder.DropIndex(
                name: "IX_ProjectAction_UserDestinationId",
                table: "ProjectAction");

            migrationBuilder.DropColumn(
                name: "UserDestinationId",
                table: "ProjectAction");

            migrationBuilder.CreateTable(
                name: "ProjectActionAssignUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectActionId = table.Column<long>(type: "bigint", nullable: false),
                    UserAssignedId = table.Column<long>(type: "bigint", nullable: false),
                    UserRoleId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectActionStatusType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActionAssignUsers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectActionAssignUsers");

            migrationBuilder.AddColumn<long>(
                name: "UserDestinationId",
                table: "ProjectAction",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAction_UserDestinationId",
                table: "ProjectAction",
                column: "UserDestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectAction_User_UserDestinationId",
                table: "ProjectAction",
                column: "UserDestinationId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
