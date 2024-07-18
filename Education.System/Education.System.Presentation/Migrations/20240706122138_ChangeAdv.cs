using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLayerProduction.Presentaion.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAdv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                schema: "App",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherName",
                schema: "App",
                table: "Advertisements");
        }
    }
}
