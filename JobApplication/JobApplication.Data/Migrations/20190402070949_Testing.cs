using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplication.Data.Migrations
{
    public partial class Testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CVs_CVId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CVId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CVId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CVId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CVId",
                table: "Users",
                column: "CVId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CVs_CVId",
                table: "Users",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
