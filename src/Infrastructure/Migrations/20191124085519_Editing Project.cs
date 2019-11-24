using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class EditingProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewProjectLocation",
                table: "StoreTypeSites");

            migrationBuilder.DropColumn(
                name: "NewProjectLocation",
                table: "BlogTypeSites");

            migrationBuilder.AddColumn<string>(
                name: "RepositoryName",
                table: "LaunchConfigs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepositoryName",
                table: "LaunchConfigs");

            migrationBuilder.AddColumn<string>(
                name: "NewProjectLocation",
                table: "StoreTypeSites",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewProjectLocation",
                table: "BlogTypeSites",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
