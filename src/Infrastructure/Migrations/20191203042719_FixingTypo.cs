using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FixingTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ClientWidjets_ClientWidjetId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_ClientWidjets_ClientWidjetId",
                table: "Widjets");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_WidgetClientWidget_Id",
                table: "WidgetClientWidget");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClientWidjetId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "WidgetClientWidget");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "WidgetClientWidget");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "WidgetClientWidget");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WidgetClientWidget");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "WidgetClientWidget");

            migrationBuilder.RenameColumn(
                name: "ClientWidjetId",
                table: "Widjets",
                newName: "ClientWidgetId");

            migrationBuilder.RenameIndex(
                name: "IX_Widjets_ClientWidjetId",
                table: "Widjets",
                newName: "IX_Widjets_ClientWidgetId");

            migrationBuilder.AlterColumn<string>(
                name: "ClientWidjetId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientWidjetsId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientWidjetsId",
                table: "AspNetUsers",
                column: "ClientWidjetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ClientWidjets_ClientWidjetsId",
                table: "AspNetUsers",
                column: "ClientWidjetsId",
                principalTable: "ClientWidjets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_ClientWidjets_ClientWidgetId",
                table: "Widjets",
                column: "ClientWidgetId",
                principalTable: "ClientWidjets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ClientWidjets_ClientWidjetsId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_ClientWidjets_ClientWidgetId",
                table: "Widjets");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClientWidjetsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientWidjetsId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ClientWidgetId",
                table: "Widjets",
                newName: "ClientWidjetId");

            migrationBuilder.RenameIndex(
                name: "IX_Widjets_ClientWidgetId",
                table: "Widjets",
                newName: "IX_Widjets_ClientWidjetId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "WidgetClientWidget",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "WidgetClientWidget",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "WidgetClientWidget",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WidgetClientWidget",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "WidgetClientWidget",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientWidjetId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_WidgetClientWidget_Id",
                table: "WidgetClientWidget",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientWidjetId",
                table: "AspNetUsers",
                column: "ClientWidjetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ClientWidjets_ClientWidjetId",
                table: "AspNetUsers",
                column: "ClientWidjetId",
                principalTable: "ClientWidjets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_ClientWidjets_ClientWidjetId",
                table: "Widjets",
                column: "ClientWidjetId",
                principalTable: "ClientWidjets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
