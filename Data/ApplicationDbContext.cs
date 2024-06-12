using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NestAlbania.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin@admin.com", // username i userit
                NormalizedUserName = "admin@admin.com", // 
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = string.Empty // property e cila merr vlere kur nje user ndryshon password, ose kur behet logout nga sistemi. (Deactivate)
                // pas kesaj duhet migrim dhe update

            });

        }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
           
            public DbSet<JobApply> JobApplications { get; set; } 
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
    
}
