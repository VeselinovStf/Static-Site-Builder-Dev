using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeSiteTypesField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateLocation",
                table: "StoreTypeSites");

            migrationBuilder.DropColumn(
                name: "TemplateLocation",
                table: "BlogTypeSites");

            migrationBuilder.AddColumn<string>(
                name: "TemplateName",
                table: "StoreTypeSites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TemplateName",
                table: "BlogTypeSites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateName",
                table: "StoreTypeSites");

            migrationBuilder.DropColumn(
                name: "TemplateName",
                table: "BlogTypeSites");

            migrationBuilder.AddColumn<string>(
                name: "TemplateLocation",
                table: "StoreTypeSites",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TemplateLocation",
                table: "BlogTypeSites",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
