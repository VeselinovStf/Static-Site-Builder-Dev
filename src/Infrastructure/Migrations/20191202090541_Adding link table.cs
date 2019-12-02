using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Addinglinktable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WidgetClientWidget",
                columns: table => new
                {
                    WidgetId = table.Column<string>(nullable: false),
                    ClientWidgetId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetClientWidget", x => new { x.ClientWidgetId, x.WidgetId });
                    table.UniqueConstraint("AK_WidgetClientWidget_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WidgetClientWidget_ClientWidjets_ClientWidgetId",
                        column: x => x.ClientWidgetId,
                        principalTable: "ClientWidjets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WidgetClientWidget_Widjets_WidgetId",
                        column: x => x.WidgetId,
                        principalTable: "Widjets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WidgetClientWidget_WidgetId",
                table: "WidgetClientWidget",
                column: "WidgetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WidgetClientWidget");
        }
    }
}
