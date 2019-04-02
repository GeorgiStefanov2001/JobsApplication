using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplication.Data.Migrations
{
    public partial class Relations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CVId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CVId",
                table: "Users",
                column: "CVId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CVs_CVId",
                table: "Users",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_CVs_CVId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CVId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CVId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects");
        }
    }
}
