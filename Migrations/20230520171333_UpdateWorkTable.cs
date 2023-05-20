using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PresentroMVC.Migrations
{
    public partial class UpdateWorkTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Works_WorkId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_WorkId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "WorkId",
                table: "Works");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkId",
                table: "Works",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorkId",
                table: "Works",
                column: "WorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Works_WorkId",
                table: "Works",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id");
        }
    }
}
