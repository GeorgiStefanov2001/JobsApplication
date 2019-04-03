using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplication.Data.Migrations
{
    public partial class RemovedJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_CVs_CVId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CVId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CVId",
                table: "Jobs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CVId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CVId",
                table: "Jobs",
                column: "CVId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_CVs_CVId",
                table: "Jobs",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
