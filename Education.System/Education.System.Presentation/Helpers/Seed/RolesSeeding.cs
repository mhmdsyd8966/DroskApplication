using Education.System.Core.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.System.Presentation.Helpers.Seed
{
    public static class RolesSeeding
    {
        public static void SeedRoles(this ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Id = "35561bfe-d346-4b70-8380-b15d23edbe2e",
                ConcurrencyStamp = "1",
                Name = Roles.Admin,
                NormalizedName = Roles.Admin.ToUpper()
            },
            new IdentityRole()
            {
                Id = "a7d62d27-150f-4165-961e-e5095e09ddb1",
                ConcurrencyStamp = "2",
                Name = Roles.Student,
                NormalizedName = Roles.Student.ToUpper()
            },
            new IdentityRole()
            {
                Id = "47470826-cdc6-4eb0-9ebe-75f10c89c1da",
                ConcurrencyStamp = "3",
                Name = Roles.Teacher,
                NormalizedName = Roles.Teacher.ToUpper()
            }
        );
        }
    }
}
