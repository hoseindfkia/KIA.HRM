using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCityAndProvince140307213 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Mission_CityId",
                table: "Mission",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_City_CityId",
                table: "Mission",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_City_CityId",
                table: "Mission");

            migrationBuilder.DropIndex(
                name: "IX_Mission_CityId",
                table: "Mission");
        }
    }
}
