using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Wallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WalletId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    AvailibleCredit = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wallet_WalletId",
                table: "AspNetUsers",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wallet_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WalletId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "AspNetUsers");
        }
    }
}
