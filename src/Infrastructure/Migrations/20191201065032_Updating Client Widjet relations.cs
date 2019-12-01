using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatingClientWidjetrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientWidjets_AspNetUsers_ApplicationUserId",
                table: "ClientWidjets");

            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_WidjetElements_WidjetElementId",
                table: "Widjets");

            migrationBuilder.DropTable(
                name: "WidjetElements");

            migrationBuilder.DropIndex(
                name: "IX_ClientWidjets_ApplicationUserId",
                table: "ClientWidjets");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ClientWidjets");

            migrationBuilder.RenameColumn(
                name: "WidjetElementId",
                table: "Widjets",
                newName: "ClientWidjetId");

            migrationBuilder.RenameIndex(
                name: "IX_Widjets_WidjetElementId",
                table: "Widjets",
                newName: "IX_Widjets_ClientWidjetId");

            migrationBuilder.AddColumn<string>(
                name: "ClientWidjetId",
                table: "AspNetUsers",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ClientWidjets_ClientWidjetId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_ClientWidjets_ClientWidjetId",
                table: "Widjets");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClientWidjetId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientWidjetId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ClientWidjetId",
                table: "Widjets",
                newName: "WidjetElementId");

            migrationBuilder.RenameIndex(
                name: "IX_Widjets_ClientWidjetId",
                table: "Widjets",
                newName: "IX_Widjets_WidjetElementId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ClientWidjets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WidjetElements",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ClientWidjetId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidjetElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WidjetElements_ClientWidjets_ClientWidjetId",
                        column: x => x.ClientWidjetId,
                        principalTable: "ClientWidjets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientWidjets_ApplicationUserId",
                table: "ClientWidjets",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WidjetElements_ClientWidjetId",
                table: "WidjetElements",
                column: "ClientWidjetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientWidjets_AspNetUsers_ApplicationUserId",
                table: "ClientWidjets",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_WidjetElements_WidjetElementId",
                table: "Widjets",
                column: "WidjetElementId",
                principalTable: "WidjetElements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
