// // // using Microsoft.AspNetCore.Identity;
// // // using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// // // using Microsoft.EntityFrameworkCore;
// // // using NestAlbania.Models;
// // //
// // // namespace NestAlbania.Data
// // // {
// // //     public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
// // //     {
// // //         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
// // //             : base(options)
// // //         {
// // //         }
// // //
// // //         public DbSet<ApplicationUser> ApplicationUsers { get; set; }
// // //         public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
// // //         public DbSet<ApplicationRole> ApplicationRoles { get; set; }
// // //
// // //         public DbSet<Property> Properties { get; set; }
// // //
// // //         public DbSet<Agent> Agents { get; set; }
// // //
// // //         
// // //         public DbSet<Notification> Notifications { get; set; }
// // //      
// // //
// // //         protected override void OnModelCreating(ModelBuilder modelBuilder)
// // //         {
// // //             base.OnModelCreating(modelBuilder); // Call base method to configure identity
// // //
// // //             const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
// // //             const string ADMIN_ROLE_ID = "b18be9c0-aa65-4af8-bd17-00bd9344e576";
// // //             const string AGENT_ROLE_ID = "a14bs9c0-aa65-4af8-bd17-00bd9344e575";
// // //             const string NormalUser_ROLE_ID = "e13fc5b7-cc45-4a6c-a8d2-02ab1298e678";
// // //
// // //             var hasher = new PasswordHasher<ApplicationUser>();
// // //             modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
// // //             {
// // //                 Id = ADMIN_ID,
// // //                 UserName = "admin@admin.com",
// // //                 NormalizedUserName = "ADMIN@ADMIN.COM",
// // //                 Email = "admin@admin.com",
// // //                 NormalizedEmail = "ADMIN@ADMIN.COM",
// // //                 EmailConfirmed = true,
// // //                 PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
// // //                 SecurityStamp = string.Empty
// // //             });
// // //
// // //             modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
// // //             {
// // //                 Id = ADMIN_ROLE_ID,
// // //                 Name = "admin",
// // //                 NormalizedName = "ADMIN"
// // //             });
// // //
// // //             modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
// // //             {
// // //                 Id = AGENT_ROLE_ID,
// // //                 Name = "Agent",
// // //                 NormalizedName = "AGENT"
// // //             });
// // //             modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
// // //             {
// // //                 Id = NormalUser_ROLE_ID,
// // //                 Name = "NormalUser",
// // //                 NormalizedName = "NORMALUSER",
// // //             });
// // //
// // //             modelBuilder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
// // //             {
// // //                 RoleId = ADMIN_ROLE_ID,
// // //                 UserId = ADMIN_ID
// // //             });
// // //
// // //
// // //
// // //             modelBuilder.Entity<Property>()
// // //                 .HasOne(p => p.Agent) 
// // //                 .WithMany(a => a.Properties)
// // //                 .HasForeignKey(p => p.AgentId)
// // //                 .OnDelete(DeleteBehavior.SetNull);
// // //
// // //
// // //
// // //         }
// // //     }
// // // }
// // //
// //
// //
// //
// //
// //
// // using Microsoft.AspNetCore.Identity;
// // using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// // using Microsoft.EntityFrameworkCore;
// // using NestAlbania.Models;
// // using System.Linq;
// //
// // namespace NestAlbania.Data
// // {
// //     public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
// //     {
// //         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
// //             : base(options)
// //         {
// //         }
// //
// //         public DbSet<ApplicationUser> ApplicationUsers { get; set; }
// //         public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
// //         public DbSet<ApplicationRole> ApplicationRoles { get; set; }
// //         public DbSet<Property> Properties { get; set; }
// //         public DbSet<Agent> Agents { get; set; }
// //         public DbSet<Notification> Notifications { get; set; }
// //
// //         protected override void OnModelCreating(ModelBuilder modelBuilder)
// //         {
// //             base.OnModelCreating(modelBuilder); // Call base method to configure identity
// //
// //             // Seeding data only if necessary
// //             SeedData(modelBuilder);
// //         }
// //
// //         private void SeedData(ModelBuilder modelBuilder)
// //         {
// //             const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
// //             const string ADMIN_ROLE_ID = "b18be9c0-aa65-4af8-bd17-00bd9344e576";
// //             const string AGENT_ROLE_ID = "a14bs9c0-aa65-4af8-bd17-00bd9344e575";
// //             const string NormalUser_ROLE_ID = "e13fc5b7-cc45-4a6c-a8d2-02ab1298e678";
// //
// //             var hasher = new PasswordHasher<ApplicationUser>();
// //
// //             // Seed roles only if they don't exist
// //             if (!ApplicationRoles.Any(r => r.Id == ADMIN_ROLE_ID))
// //             {
// //                 modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
// //                 {
// //                     Id = ADMIN_ROLE_ID,
// //                     Name = "admin",
// //                     NormalizedName = "ADMIN"
// //                 });
// //             }
// //
// //             if (!ApplicationRoles.Any(r => r.Id == AGENT_ROLE_ID))
// //             {
// //                 modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
// //                 {
// //                     Id = AGENT_ROLE_ID,
// //                     Name = "Agent",
// //                     NormalizedName = "AGENT"
// //                 });
// //             }
// //
// //             if (!ApplicationRoles.Any(r => r.Id == NormalUser_ROLE_ID))
// //             {
// //                 modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
// //                 {
// //                     Id = NormalUser_ROLE_ID,
// //                     Name = "NormalUser",
// //                     NormalizedName = "NORMALUSER",
// //                 });
// //             }
// //
// //             // Seed the admin user only if it doesn't exist
// //             if (!ApplicationUsers.Any(u => u.Id == ADMIN_ID))
// //             {
// //                 modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
// //                 {
// //                     Id = ADMIN_ID,
// //                     UserName = "admin@admin.com",
// //                     NormalizedUserName = "ADMIN@ADMIN.COM",
// //                     Email = "admin@admin.com",
// //                     NormalizedEmail = "ADMIN@ADMIN.COM",
// //                     EmailConfirmed = true,
// //                     PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
// //                     SecurityStamp = string.Empty
// //                 });
// //
// //                 modelBuilder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
// //                 {
// //                     RoleId = ADMIN_ROLE_ID,
// //                     UserId = ADMIN_ID
// //                 });
// //             }
// //
// //             modelBuilder.Entity<Property>()
// //                 .HasOne(p => p.Agent) 
// //                 .WithMany(a => a.Properties)
// //                 .HasForeignKey(p => p.AgentId)
// //                 .OnDelete(DeleteBehavior.SetNull);
// //         }
// //     }
// // }
//
//

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NestAlbania.Models;

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
        public DbSet<Property> Properties { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base method to configure identity

            // Seed data only if necessary
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ADMIN_ROLE_ID = "b18be9c0-aa65-4af8-bd17-00bd9344e576";
            const string AGENT_ROLE_ID = "a14bs9c0-aa65-4af8-bd17-00bd9344e575";
            const string NORMAL_USER_ROLE_ID = "e13fc5b7-cc45-4a6c-a8d2-02ab1298e678";

            var hasher = new PasswordHasher<ApplicationUser>();

            // Seed roles only if they don't exist
            modelBuilder.Entity<ApplicationRole>().HasData(new[]
            {
                new ApplicationRole { Id = ADMIN_ROLE_ID, Name = "admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Id = AGENT_ROLE_ID, Name = "Agent", NormalizedName = "AGENT" },
                new ApplicationRole { Id = NORMAL_USER_ROLE_ID, Name = "NormalUser", NormalizedName = "NORMALUSER" }
            });

            // Seed the admin user only if it doesn't exist
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
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

            // User-Role relationship
            modelBuilder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });

            // Configure relationships
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Agent) 
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}


    




















//
//
//
// using Microsoft.AspNetCore.Identity;
//     using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//     using Microsoft.EntityFrameworkCore;
//     using NestAlbania.Models;
//     using System.Threading.Tasks;
//
//     namespace NestAlbania.Data
//     {
//         public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
//         {
//             public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//                 : base(options)
//             {
//                 ChangeTracker.LazyLoadingEnabled = false;
//                 ChangeTracker.AutoDetectChangesEnabled = false;
//             }
//
//             public DbSet<ApplicationUser> ApplicationUsers { get; set; }
//             public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
//             public DbSet<ApplicationRole> ApplicationRoles { get; set; }
//             public DbSet<Property> Properties { get; set; }
//             public DbSet<Agent> Agents { get; set; }
//             public DbSet<Notification> Notifications { get; set; }
//
//             protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//             {
//                 optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//                 base.OnConfiguring(optionsBuilder);
//             }
//
//             protected override void OnModelCreating(ModelBuilder modelBuilder)
//             {
//                 base.OnModelCreating(modelBuilder);
//
//                 // Configure relationships
//                 modelBuilder.Entity<Property>()
//                     .HasOne(p => p.Agent) 
//                     .WithMany(a => a.Properties)
//                     .HasForeignKey(p => p.AgentId)
//                     .OnDelete(DeleteBehavior.SetNull);
//
//                 // Add indexes
//                 modelBuilder.Entity<ApplicationUser>()
//                     .HasIndex(u => u.NormalizedUserName);
//                 modelBuilder.Entity<ApplicationUser>()
//                     .HasIndex(u => u.NormalizedEmail);
//             }
//
//             public async Task EnsureDatabaseSetupAsync()
//             {
//                 if ((await Database.GetPendingMigrationsAsync()).Any())
//                 {
//                     await Database.MigrateAsync();
//                 }
//
//                 await EnsureSeedDataAsync();
//             }
//
//             public async Task EnsureSeedDataAsync()
//             {
//                 if (!await ApplicationRoles.AnyAsync())
//                 {
//                     await SeedRolesAsync();
//                 }
//
//                 if (!await ApplicationUsers.AnyAsync(u => u.UserName == "admin@admin.com"))
//                 {
//                     await SeedAdminUserAsync();
//                 }
//             }
//
//             private async Task SeedRolesAsync()
//             {
//                 await ApplicationRoles.AddRangeAsync(new[]
//                 {
//                     new ApplicationRole { Id = "b18be9c0-aa65-4af8-bd17-00bd9344e576", Name = "admin", NormalizedName = "ADMIN" },
//                     new ApplicationRole { Id = "a14bs9c0-aa65-4af8-bd17-00bd9344e575", Name = "Agent", NormalizedName = "AGENT" },
//                     new ApplicationRole { Id = "e13fc5b7-cc45-4a6c-a8d2-02ab1298e678", Name = "NormalUser", NormalizedName = "NORMALUSER" }
//                 });
//                 await SaveChangesAsync();
//             }
//
//             private async Task SeedAdminUserAsync()
//             {
//                 var hasher = new PasswordHasher<ApplicationUser>();
//                 var adminUser = new ApplicationUser
//                 {
//                     Id = "a18be9c0-aa65-4af8-bd17-00bd9344e575",
//                     UserName = "admin@admin.com",
//                     NormalizedUserName = "ADMIN@ADMIN.COM",
//                     Email = "admin@admin.com",
//                     NormalizedEmail = "ADMIN@ADMIN.COM",
//                     EmailConfirmed = true,
//                     PasswordHash = hasher.HashPassword(null, "P@ssw0rd"),
//                     SecurityStamp = string.Empty
//                 };
//
//                 await ApplicationUsers.AddAsync(adminUser);
//                 await ApplicationUserRoles.AddAsync(new ApplicationUserRole
//                 {
//                     RoleId = "b18be9c0-aa65-4af8-bd17-00bd9344e576",
//                     UserId = adminUser.Id
//                 });
//                 await SaveChangesAsync();
//             }
//         }
//     }