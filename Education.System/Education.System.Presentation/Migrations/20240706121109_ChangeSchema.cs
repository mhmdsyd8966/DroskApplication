using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLayerProduction.Presentaion.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Advertisements",
                schema: "Security",
                newName: "Advertisements",
                newSchema: "App");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Advertisements",
                schema: "App",
                newName: "Advertisements",
                newSchema: "Security");
        }
    }
}
