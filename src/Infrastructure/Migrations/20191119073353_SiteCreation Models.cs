using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class SiteCreationModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Messages",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "SendDate",
                table: "Messages",
                newName: "Send Date Of Message");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "Messages",
                newName: "Sender");

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "Messages",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Messages",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Messages",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sender",
                table: "Messages",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientWidjets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ClientId = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientWidjets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientWidjets_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LaunchConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    IsLaunched = table.Column<bool>(nullable: false),
                    CardApiKey = table.Column<string>(nullable: true),
                    CardServiceGate = table.Column<string>(nullable: true),
                    HostingServiceGate = table.Column<string>(nullable: true),
                    Repository = table.Column<string>(nullable: true),
                    ClientId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaunchConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
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
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseSiteProject",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    NewProjectLocation = table.Column<string>(maxLength: 100, nullable: false),
                    TemplateLocation = table.Column<string>(maxLength: 100, nullable: false),
                    ClientId = table.Column<string>(nullable: true),
                    LaunchingConfigId = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true),
                    StoreTypeSite_ProjectId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseSiteProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseSiteProject_LaunchConfigs_LaunchingConfigId",
                        column: x => x.LaunchingConfigId,
                        principalTable: "LaunchConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseSiteProject_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseSiteProject_Projects_StoreTypeSite_ProjectId",
                        column: x => x.StoreTypeSite_ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Header = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    PubDate = table.Column<DateTime>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    AuthorName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_BaseSiteProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "BaseSiteProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    DiscountProcent = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    DiscountTimer = table.Column<DateTime>(nullable: false),
                    TemplateId = table.Column<int>(nullable: false),
                    Brand = table.Column<string>(nullable: true),
                    DetailsLink = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true),
                    State = table.Column<bool>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Collection = table.Column<string>(nullable: true),
                    Hot = table.Column<bool>(nullable: false),
                    LatestProduct = table.Column<bool>(nullable: false),
                    LatestNewCollectionProduct = table.Column<bool>(nullable: false),
                    DealOfTheDayProduct = table.Column<bool>(nullable: false),
                    ProductOfTheDay = table.Column<bool>(nullable: false),
                    PickedForYou = table.Column<bool>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Publish = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    ProjectId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_BaseSiteProject_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "BaseSiteProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WidjetElements",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Functionality = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Votes = table.Column<double>(nullable: false),
                    IsOn = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    IsFree = table.Column<bool>(nullable: false),
                    ClientWidjetId = table.Column<string>(nullable: true),
                    AvailibleSiteWidjetId = table.Column<string>(nullable: true),
                    UsedSiteWidjetId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidjetElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WidjetElements_BaseSiteProject_AvailibleSiteWidjetId",
                        column: x => x.AvailibleSiteWidjetId,
                        principalTable: "BaseSiteProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WidjetElements_ClientWidjets_ClientWidjetId",
                        column: x => x.ClientWidjetId,
                        principalTable: "ClientWidjets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WidjetElements_BaseSiteProject_UsedSiteWidjetId",
                        column: x => x.UsedSiteWidjetId,
                        principalTable: "BaseSiteProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostFrontMatters",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Layout = table.Column<string>(maxLength: 100, nullable: false),
                    PermaLink = table.Column<string>(maxLength: 100, nullable: true),
                    Include = table.Column<string>(maxLength: 100, nullable: true),
                    BlogPostId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostFrontMatters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPostFrontMatters_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductsFrontMatters",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Layout = table.Column<string>(maxLength: 100, nullable: false),
                    PermaLink = table.Column<string>(maxLength: 100, nullable: true),
                    Include = table.Column<string>(maxLength: 100, nullable: true),
                    ProductId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsFrontMatters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsFrontMatters_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProjectId",
                table: "AspNetUsers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSiteProject_LaunchingConfigId",
                table: "BaseSiteProject",
                column: "LaunchingConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSiteProject_ProjectId",
                table: "BaseSiteProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseSiteProject_StoreTypeSite_ProjectId",
                table: "BaseSiteProject",
                column: "StoreTypeSite_ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostFrontMatters_BlogPostId",
                table: "BlogPostFrontMatters",
                column: "BlogPostId",
                unique: true,
                filter: "[BlogPostId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_ProjectId",
                table: "BlogPosts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientWidjets_ApplicationUserId",
                table: "ClientWidjets",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProjectId",
                table: "Products",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsFrontMatters_ProductId",
                table: "ProductsFrontMatters",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WidjetElements_AvailibleSiteWidjetId",
                table: "WidjetElements",
                column: "AvailibleSiteWidjetId");

            migrationBuilder.CreateIndex(
                name: "IX_WidjetElements_ClientWidjetId",
                table: "WidjetElements",
                column: "ClientWidjetId");

            migrationBuilder.CreateIndex(
                name: "IX_WidjetElements_UsedSiteWidjetId",
                table: "WidjetElements",
                column: "UsedSiteWidjetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectId",
                table: "AspNetUsers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Projects_ProjectId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BlogPostFrontMatters");

            migrationBuilder.DropTable(
                name: "ProductsFrontMatters");

            migrationBuilder.DropTable(
                name: "WidjetElements");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ClientWidjets");

            migrationBuilder.DropTable(
                name: "BaseSiteProject");

            migrationBuilder.DropTable(
                name: "LaunchConfigs");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProjectId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Messages",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Send Date Of Message",
                table: "Messages",
                newName: "SendDate");

            migrationBuilder.RenameColumn(
                name: "Sender",
                table: "Messages",
                newName: "From");

            migrationBuilder.AlterColumn<string>(
                name: "To",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "From",
                table: "Messages",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
