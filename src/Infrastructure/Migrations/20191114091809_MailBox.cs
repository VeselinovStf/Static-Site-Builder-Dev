using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class MailBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MailBoxId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MailBoxes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ClientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailBoxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: false),
                    IsDraft = table.Column<bool>(nullable: false),
                    IsTrash = table.Column<bool>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    MailBoxId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_MailBoxes_MailBoxId",
                        column: x => x.MailBoxId,
                        principalTable: "MailBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MailBoxId",
                table: "AspNetUsers",
                column: "MailBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MailBoxId",
                table: "Messages",
                column: "MailBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MailBoxes_MailBoxId",
                table: "AspNetUsers",
                column: "MailBoxId",
                principalTable: "MailBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MailBoxes_MailBoxId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MailBoxes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MailBoxId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MailBoxId",
                table: "AspNetUsers");
        }
    }
}
