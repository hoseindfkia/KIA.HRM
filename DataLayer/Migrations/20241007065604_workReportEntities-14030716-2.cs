using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class workReportEntities140307162 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCreator",
                table: "PreparationDocument");

            migrationBuilder.DropColumn(
                name: "UserCreator",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "UserCreator",
                table: "Meeting");

            migrationBuilder.DropColumn(
                name: "UserCreator",
                table: "Leave");

            migrationBuilder.AlterColumn<long>(
                name: "ApproverUserId",
                table: "PreparationDocument",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserCreatorId",
                table: "PreparationDocument",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApproverUserId",
                table: "Mission",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserCreatorId",
                table: "Mission",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApproverUserId",
                table: "Meeting",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserCreatorId",
                table: "Meeting",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ApproverUserId",
                table: "Leave",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "UserCreatorId",
                table: "Leave",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "PreparationDocument");

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Meeting");

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Leave");

            migrationBuilder.AlterColumn<long>(
                name: "ApproverUserId",
                table: "PreparationDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserCreator",
                table: "PreparationDocument",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ApproverUserId",
                table: "Mission",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserCreator",
                table: "Mission",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ApproverUserId",
                table: "Meeting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserCreator",
                table: "Meeting",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ApproverUserId",
                table: "Leave",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserCreator",
                table: "Leave",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
