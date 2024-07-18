using Education.System.Core.Application;
using Education.System.Core.Identity;
using Education.System.Core.Identity.Base;
using Education.System.Core.Views;
using Education.System.Presentation.Helpers.Seed;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Education.System.Presentation.Context
{
    public class TheLayerContext(DbContextOptions<TheLayerContext> options) : IdentityDbContext<Consumer>(options)
    {
        //Application
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Package> Packages { get; set; }



        public DbSet<Advertisement> Advertisements { set; get; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<BugReport> BugReports { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Exam> Exams { set; get; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CallSupport> CallSupports { get; set; }


        public DbSet<PackageStudent> PackageStudents { get; set; }

        //identity
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Consumer> Consumers { get; set; }

        // //views
        public DbSet<PackageLesson> PackageLessons { get; set; }
        public DbSet<TeacherPackage> TeacherPackages { get; set; }
        public DbSet<TeacherPost> TeacherPosts { get; set; }
        public DbSet<StudentPackage> StudentPackages { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Security");
            //Application 
            builder.Entity<Course>()
                .ToTable("Courses", "App");
            builder.Entity<Lesson>()
                .ToTable("Lessons", "App");
            builder.Entity<PackageStudent>()
                .HasNoKey();
            builder.Entity<Advertisement>()
                .ToTable($"{nameof(Advertisement)}s", "App");
            builder.Entity<Package>()
                .ToTable("Packages", "App");
            builder.Entity<Post>()
                .ToTable("Posts", "App");
            builder.Entity<Discount>()
                .ToTable("Discounts", "App");
            builder.Entity<BugReport>()
                .ToTable("BugReports", "App");
            builder.Entity<Transaction>()
                .ToTable("Transactions", "App");
            builder.Entity<CallSupport>()
                .ToTable("CallSupports", "App");
            builder.Entity<Exam>()
                .ToTable("Exams", "App");

            builder.Entity<Admin>();
            builder.Entity<Student>();
            builder.Entity<Teacher>();
            builder.Entity<Consumer>().ToTable("Consumers");

            builder.Entity<PackageLesson>()
                .ToView("PackageLessons", "App")
                .HasNoKey();
            builder.Entity<TeacherPackage>()
                .ToView("TeacherPackages", "App")
                .HasNoKey();
            builder.Entity<TeacherPost>()
                .ToView("TeacherPosts", "App")
                .HasNoKey();
            builder.Entity<StudentPackage>()
                .ToView("StudentPackages", "App")
                .HasNoKey();

            builder.SeedCourses();
            builder.SeedRoles();
        }
    }
}
