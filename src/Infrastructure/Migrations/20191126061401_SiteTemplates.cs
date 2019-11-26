using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class SiteTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HostingId",
                table: "LaunchConfigs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SiteTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    SiteTypeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteTemplates_SiteTypes_SiteTypeId",
                        column: x => x.SiteTypeId,
                        principalTable: "SiteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteTemplateElements",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    FileContent = table.Column<string>(nullable: true),
                    SiteTemplateId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteTemplateElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteTemplateElements_SiteTemplates_SiteTemplateId",
                        column: x => x.SiteTemplateId,
                        principalTable: "SiteTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SiteTemplateElements_SiteTemplateId",
                table: "SiteTemplateElements",
                column: "SiteTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteTemplates_SiteTypeId",
                table: "SiteTemplates",
                column: "SiteTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SiteTemplateElements");

            migrationBuilder.DropTable(
                name: "SiteTemplates");

            migrationBuilder.DropColumn(
                name: "HostingId",
                table: "LaunchConfigs");
        }
    }
}
