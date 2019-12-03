using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ApplicationUserWidgetsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ClientWidjets_ClientWidjetsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientWidgets_ClientWidjets_ApplicationUserWidgetsId",
                table: "ClientWidgets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientWidjets",
                table: "ClientWidjets");

            migrationBuilder.RenameTable(
                name: "ClientWidjets",
                newName: "ApplicationUserWidgets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserWidgets",
                table: "ApplicationUserWidgets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ApplicationUserWidgets_ClientWidjetsId",
                table: "AspNetUsers",
                column: "ClientWidjetsId",
                principalTable: "ApplicationUserWidgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientWidgets_ApplicationUserWidgets_ApplicationUserWidgetsId",
                table: "ClientWidgets",
                column: "ApplicationUserWidgetsId",
                principalTable: "ApplicationUserWidgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ApplicationUserWidgets_ClientWidjetsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientWidgets_ApplicationUserWidgets_ApplicationUserWidgetsId",
                table: "ClientWidgets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserWidgets",
                table: "ApplicationUserWidgets");

            migrationBuilder.RenameTable(
                name: "ApplicationUserWidgets",
                newName: "ClientWidjets");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientWidjets",
                table: "ClientWidjets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ClientWidjets_ClientWidjetsId",
                table: "AspNetUsers",
                column: "ClientWidjetsId",
                principalTable: "ClientWidjets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientWidgets_ClientWidjets_ApplicationUserWidgetsId",
                table: "ClientWidgets",
                column: "ApplicationUserWidgetsId",
                principalTable: "ClientWidjets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
