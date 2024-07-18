using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLayerProduction.Presentaion.Migrations
{
    /// <inheritdoc />
    public partial class Add_Advertisment_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertisements",
                schema: "Security",
                columns: table => new
                {
                    PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PackagePrice = table.Column<double>(type: "float", nullable: false),
                    TeacherId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackagePhoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfStudentsWithPackage = table.Column<int>(type: "int", nullable: false),
                    NumberOfLessonsInPackage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.PackageId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements",
                schema: "Security");
        }
    }
}
