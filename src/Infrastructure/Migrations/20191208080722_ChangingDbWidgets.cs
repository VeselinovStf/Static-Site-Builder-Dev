using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangingDbWidgets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_SiteWidget_BaseWidget_SiteId",
                table: "SiteWidget");

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
                table: "SiteWidget",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoreTypeSiteId",
                table: "SiteWidget",
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
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    TemplateName = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    LaunchingConfigId = table.Column<string>(nullable: true),
                    ProjectId = table.Column<string>(nullable: true)
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
                name: "IX_SiteWidget_BlogTypeSiteId",
                table: "SiteWidget",
                column: "BlogTypeSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteWidget_StoreTypeSiteId",
                table: "SiteWidget",
                column: "StoreTypeSiteId");

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
                name: "FK_SiteWidget_BlogTypeSites_BlogTypeSiteId",
                table: "SiteWidget",
                column: "BlogTypeSiteId",
                principalTable: "BlogTypeSites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SiteWidget_StoreTypeSites_StoreTypeSiteId",
                table: "SiteWidget",
                column: "StoreTypeSiteId",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_BlogTypeSites_ProjectId",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_StoreTypeSites_ProjectId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteWidget_BlogTypeSites_BlogTypeSiteId",
                table: "SiteWidget");

            migrationBuilder.DropForeignKey(
                name: "FK_SiteWidget_StoreTypeSites_StoreTypeSiteId",
                table: "SiteWidget");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreTypeSites_LaunchConfigs_LaunchingConfigId",
                table: "StoreTypeSites");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreTypeSites_Projects_ProjectId",
                table: "StoreTypeSites");

            migrationBuilder.DropTable(
                name: "BlogTypeSites");

            migrationBuilder.DropIndex(
                name: "IX_SiteWidget_BlogTypeSiteId",
                table: "SiteWidget");

            migrationBuilder.DropIndex(
                name: "IX_SiteWidget_StoreTypeSiteId",
                table: "SiteWidget");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreTypeSites",
                table: "StoreTypeSites");

            migrationBuilder.DropColumn(
                name: "BlogTypeSiteId",
                table: "SiteWidget");

            migrationBuilder.DropColumn(
                name: "StoreTypeSiteId",
                table: "SiteWidget");

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

            migrationBuilder.CreateIndex(
                name: "IX_BaseWidget_ProjectId",
                table: "BaseWidget",
                column: "ProjectId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SiteWidget_BaseWidget_SiteId",
                table: "SiteWidget",
                column: "SiteId",
                principalTable: "BaseWidget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
