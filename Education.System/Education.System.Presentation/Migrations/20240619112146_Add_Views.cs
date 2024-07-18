using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheLayerProduction.Presentaion.Migrations
{
    /// <inheritdoc />
    public partial class Add_Views : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                $"CREATE or ALTER VIEW [App].[PackageLessons] AS\nSELECT \np.[Id] as PackageId,\np.[PackageName] as PackageName,\np.[PackagePhoto] as PackagePhoto,\np.[TeacherId] as TeacherId,\nl.[Id] as LessonId,\nl.[Name] as LessonName,\nl.[LessonImage] as LessonPhoto,\nl.[PdfLink] as LessonPdf,\nl.[VideoLink] as LessonVideo\nFROM \n[App].[Packages] as p \nINNER JOIN\n[App].[Lessons] as l\non l.[PackageId] = p.Id\n");
            migrationBuilder.Sql(
                $"CREATE OR ALTER VIEW [App].[StudentPackages] AS\nSELECT\n    s.[Id] AS StudentId,\n    CONCAT(s.[FirstName], ' ', s.[LastName]) AS StudentName,\n    p.[Id] AS PackageId,\n    p.[PackageName] AS PackageName,\n    p.[PackagePhoto] AS PackagePhoto,\n    p.NumberOfLessons AS NumberOfLessons\nFROM\n    [Security].[Consumers] AS s\nINNER JOIN\n    [Security].[PackageStudent] AS ps ON ps.StudentsId = s.Id\nINNER JOIN\n    [App].[Packages] AS p ON ps.PackagesId = p.Id;\n");
            migrationBuilder.Sql(
                $"CREATE OR ALTER VIEW [App].[TeacherPackages] AS\nSelect \nt.Id as TeacherId,\nCONCAT(t.[FirstName], ' ', t.[LastName]) as TeacherName,\nt.Photo as TeacherPhoto,\np.Id as PackageId,\np.PackageName as PackageName,\np.NumberOfLessons as NumberOfLessons,\np.PackagePhoto as PackagePhoto,\np.PackagePrice as PackagePrice,\np.NumberOfStudents as NumberOfStudents\nFROM\n[Security].[Consumers] as t \nINNER JOIN \n[App].[Packages] as p \non p.TeacherId = t.Id");
            migrationBuilder.Sql(
                $"CREATE OR ALTER VIEW [App].[TeacherPosts] AS\nSELECT\np.Id as PostId,\np.CreatedAt as CreatedAt,\np.Content as Contents,\np.PostPhoto as PostPhoto,\nt.Id as TeacherId,\nCONCAT(t.[FirstName], ' ', t.[LastName]) as TeacherName,\nt.Photo as TeacherPhoto\nFROM\n[Security].[Consumers] as t \nINNER JOIN \n[App].[Posts] as p \non p.TeacherId = t.Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW PackageLessons;");
            migrationBuilder.Sql("DROP VIEW StudentPackages;");
            migrationBuilder.Sql("DROP VIEW TeacherPackage;");
            migrationBuilder.Sql("DROP VIEW TeacherPost;");
        }
    }
}
