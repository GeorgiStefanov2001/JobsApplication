using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplication.Data.Migrations
{
    public partial class FixingRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CVId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CVId",
                table: "Users",
                column: "CVId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CVId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CVId",
                table: "Users",
                column: "CVId");
        }
    }
}
