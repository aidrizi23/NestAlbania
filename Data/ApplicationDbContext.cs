//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace NestAlbania.Data
//{
//    public class ApplicationDbContext : IdentityDbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
//        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
//        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
//        public DbSet<JobApply> JobApplications { get; set; } 



//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
//            var hasher = new PasswordHasher<ApplicationUser>();
//            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
//            {
//                Id = ADMIN_ID,
//                UserName = "admin@admin.com", 
//                NormalizedUserName = "admin@admin.com", 
//                Email = "admin@admin.com",
//                NormalizedEmail = "admin@admin.com",
//                EmailConfirmed = true,
//                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
//                SecurityStamp = string.Empty 

//            });


//            const string ROLE_ID = ADMIN_ID;
//            builder.Entity<ApplicationRole>().HasData(new ApplicationRole
//            {
//                Id = ROLE_ID,
//                Name = "admin",
//                NormalizedName = "admin"
//            });

//            builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
//            {
//                RoleId = ROLE_ID,
//                UserId = ADMIN_ID
//            });

//        }

//    }

//}


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NestAlbania.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<JobApply> JobApplications { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Country> Countries { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ROLE_ID = "b18be9c0-aa65-4af8-bd17-00bd9344e576";

            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = string.Empty
            });

            builder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}

