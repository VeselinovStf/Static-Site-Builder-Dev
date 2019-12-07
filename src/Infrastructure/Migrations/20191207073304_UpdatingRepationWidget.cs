using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UpdatingRepationWidget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogTypeSites_ProjectId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_StoreTypeSites_ProjectId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreTypeSites_LaunchConfigs_LaunchingConfigId",
                table: "StoreTypeSites");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreTypeSites_Projects_ProjectId",
                table: "StoreTypeSites");

            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_BlogTypeSites_BlogTypeSiteId",
                table: "Widjets");

            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_BlogTypeSites_BlogTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_StoreTypeSites_StoreTypeSiteId",
                table: "Widjets");

            migrationBuilder.DropForeignKey(
                name: "FK_Widjets_StoreTypeSites_StoreTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropTable(
                name: "BlogTypeSites");

            migrationBuilder.DropIndex(
                name: "IX_Widjets_BlogTypeSiteId",
                table: "Widjets");

            migrationBuilder.DropIndex(
                name: "IX_Widjets_BlogTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropIndex(
                name: "IX_Widjets_StoreTypeSiteId",
                table: "Widjets");

            migrationBuilder.DropIndex(
                name: "IX_Widjets_StoreTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreTypeSites",
                table: "StoreTypeSites");

            migrationBuilder.DropColumn(
                name: "BlogTypeSiteId",
                table: "Widjets");

            migrationBuilder.DropColumn(
                name: "BlogTypeSiteId1",
                table: "Widjets");

            migrationBuilder.DropColumn(
                name: "StoreTypeSiteId",
                table: "Widjets");

            migrationBuilder.DropColumn(
                name: "StoreTypeSiteId1",
                table: "Widjets");

            migrationBuilder.RenameTable(
                name: "StoreTypeSites",
                newName: "BaseWidget");

            migrationBuilder.RenameIndex(
                name: "IX_StoreTypeSites_ProjectId",
                table: "BaseWidget",
                newName: "IX_BaseWidget_ProjectId1");

            migrationBuilder.RenameIndex(
                name: "IX_StoreTypeSites_LaunchingConfigId",
                table: "BaseWidget",
                newName: "IX_BaseWidget_LaunchingConfigId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseWidget",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseWidget",
                table: "BaseWidget",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SiteTypeWidget",
                columns: table => new
                {
                    WidgetId = table.Column<string>(nullable: false),
                    SiteTypeId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteTypeWidget", x => new { x.WidgetId, x.SiteTypeId });
                    table.ForeignKey(
                        name: "FK_SiteTypeWidget_SiteTypes_SiteTypeId",
                        column: x => x.SiteTypeId,
                        principalTable: "SiteTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteTypeWidget_Widjets_WidgetId",
                        column: x => x.WidgetId,
                        principalTable: "Widjets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SiteWidget",
                columns: table => new
                {
                    WidgetId = table.Column<string>(nullable: false),
                    SiteId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteWidget", x => new { x.SiteId, x.WidgetId });
                    table.ForeignKey(
                        name: "FK_SiteWidget_BaseWidget_SiteId",
                        column: x => x.SiteId,
                        principalTable: "BaseWidget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SiteWidget_Widjets_WidgetId",
                        column: x => x.WidgetId,
                        principalTable: "Widjets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseWidget_ProjectId",
                table: "BaseWidget",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteTypeWidget_SiteTypeId",
                table: "SiteTypeWidget",
                column: "SiteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteWidget_WidgetId",
                table: "SiteWidget",
                column: "WidgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseWidget_LaunchConfigs_LaunchingConfigId",
                table: "BaseWidget",
                column: "LaunchingConfigId",
                principalTable: "LaunchConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseWidget_Projects_ProjectId",
                table: "BaseWidget",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseWidget_Projects_ProjectId1",
                table: "BaseWidget",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BaseWidget_ProjectId",
                table: "BlogPosts",
                column: "ProjectId",
                principalTable: "BaseWidget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BaseWidget_ProjectId",
                table: "Products",
                column: "ProjectId",
                principalTable: "BaseWidget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseWidget_LaunchConfigs_LaunchingConfigId",
                table: "BaseWidget");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseWidget_Projects_ProjectId",
                table: "BaseWidget");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseWidget_Projects_ProjectId1",
                table: "BaseWidget");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BaseWidget_ProjectId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_BaseWidget_ProjectId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "SiteTypeWidget");

            migrationBuilder.DropTable(
                name: "SiteWidget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseWidget",
                table: "BaseWidget");

            migrationBuilder.DropIndex(
                name: "IX_BaseWidget_ProjectId",
                table: "BaseWidget");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseWidget");

            migrationBuilder.RenameTable(
                name: "BaseWidget",
                newName: "StoreTypeSites");

            migrationBuilder.RenameIndex(
                name: "IX_BaseWidget_ProjectId1",
                table: "StoreTypeSites",
                newName: "IX_StoreTypeSites_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseWidget_LaunchingConfigId",
                table: "StoreTypeSites",
                newName: "IX_StoreTypeSites_LaunchingConfigId");

            migrationBuilder.AddColumn<string>(
                name: "BlogTypeSiteId",
                table: "Widjets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlogTypeSiteId1",
                table: "Widjets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreTypeSiteId",
                table: "Widjets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreTypeSiteId1",
                table: "Widjets",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreTypeSites",
                table: "StoreTypeSites",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BlogTypeSites",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ClientId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LaunchingConfigId = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    TemplateName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTypeSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogTypeSites_LaunchConfigs_LaunchingConfigId",
                        column: x => x.LaunchingConfigId,
                        principalTable: "LaunchConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlogTypeSites_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Widjets_BlogTypeSiteId",
                table: "Widjets",
                column: "BlogTypeSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Widjets_BlogTypeSiteId1",
                table: "Widjets",
                column: "BlogTypeSiteId1");

            migrationBuilder.CreateIndex(
                name: "IX_Widjets_StoreTypeSiteId",
                table: "Widjets",
                column: "StoreTypeSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Widjets_StoreTypeSiteId1",
                table: "Widjets",
                column: "StoreTypeSiteId1");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTypeSites_LaunchingConfigId",
                table: "BlogTypeSites",
                column: "LaunchingConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTypeSites_ProjectId",
                table: "BlogTypeSites",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_BlogTypeSites_ProjectId",
                table: "BlogPosts",
                column: "ProjectId",
                principalTable: "BlogTypeSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_StoreTypeSites_ProjectId",
                table: "Products",
                column: "ProjectId",
                principalTable: "StoreTypeSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreTypeSites_LaunchConfigs_LaunchingConfigId",
                table: "StoreTypeSites",
                column: "LaunchingConfigId",
                principalTable: "LaunchConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreTypeSites_Projects_ProjectId",
                table: "StoreTypeSites",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_BlogTypeSites_BlogTypeSiteId",
                table: "Widjets",
                column: "BlogTypeSiteId",
                principalTable: "BlogTypeSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_BlogTypeSites_BlogTypeSiteId1",
                table: "Widjets",
                column: "BlogTypeSiteId1",
                principalTable: "BlogTypeSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_StoreTypeSites_StoreTypeSiteId",
                table: "Widjets",
                column: "StoreTypeSiteId",
                principalTable: "StoreTypeSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Widjets_StoreTypeSites_StoreTypeSiteId1",
                table: "Widjets",
                column: "StoreTypeSiteId1",
                principalTable: "StoreTypeSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
