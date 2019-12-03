using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatingWidgetsStructureModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_ClientWidjets_ClientWidgetId",
                table: "Widjets");

            migrationBuilder.DropTable(
                name: "WidgetClientWidget");

            migrationBuilder.DropIndex(
                name: "IX_Widjets_ClientWidgetId",
                table: "Widjets");

            migrationBuilder.DropColumn(
                name: "ClientWidgetId",
                table: "Widjets");

            migrationBuilder.CreateTable(
                name: "ClientWidgets",
                columns: table => new
                {
                    WidgetId = table.Column<string>(nullable: false),
                    ApplicationUserWidgetsId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientWidgets", x => new { x.ApplicationUserWidgetsId, x.WidgetId });
                    table.ForeignKey(
                        name: "FK_ClientWidgets_ClientWidjets_ApplicationUserWidgetsId",
                        column: x => x.ApplicationUserWidgetsId,
                        principalTable: "ClientWidjets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientWidgets_Widjets_WidgetId",
                        column: x => x.WidgetId,
                        principalTable: "Widjets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientWidgets_WidgetId",
                table: "ClientWidgets",
                column: "WidgetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientWidgets");

            migrationBuilder.AddColumn<string>(
                name: "ClientWidgetId",
                table: "Widjets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WidgetClientWidget",
                columns: table => new
                {
                    ClientWidgetId = table.Column<string>(nullable: false),
                    WidgetId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetClientWidget", x => new { x.ClientWidgetId, x.WidgetId });
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
                name: "IX_Widjets_ClientWidgetId",
                table: "Widjets",
                column: "ClientWidgetId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetClientWidget_WidgetId",
                table: "WidgetClientWidget",
                column: "WidgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_ClientWidjets_ClientWidgetId",
                table: "Widjets",
                column: "ClientWidgetId",
                principalTable: "ClientWidjets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
