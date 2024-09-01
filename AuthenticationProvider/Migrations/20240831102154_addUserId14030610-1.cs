using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationProvider.Migrations
{
    /// <inheritdoc />
    public partial class addUserId140306101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalCode",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "KIAHRM_UserId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KIAHRM_UserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                table: "AspNetUsers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: true);
        }
    }
}
