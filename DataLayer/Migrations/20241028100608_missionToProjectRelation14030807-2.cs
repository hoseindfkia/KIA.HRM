using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class missionToProjectRelation140308072 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Mission_ProjectId",
                table: "Mission",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_Project_ProjectId",
                table: "Mission",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_Project_ProjectId",
                table: "Mission");

            migrationBuilder.DropIndex(
                name: "IX_Mission_ProjectId",
                table: "Mission");
        }
    }
}
