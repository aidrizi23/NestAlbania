﻿using Microsoft.AspNetCore.Identity;
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
        public DbSet<JobApply> JobApplications { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Agent> Agents { get; set; }

     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base method to configure identity

            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string ADMIN_ROLE_ID = "b18be9c0-aa65-4af8-bd17-00bd9344e576";
            const string AGENT_ROLE_ID = "a14bs9c0-aa65-4af8-bd17-00bd9344e575";
            const string NormalUser_ROLE_ID = "e13fc5b7-cc45-4a6c-a8d2-02ab1298e678";

            var hasher = new PasswordHasher<ApplicationUser>();
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

            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = AGENT_ROLE_ID,
                Name = "Agent",
                NormalizedName = "AGENT"
            });
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole
            {
                Id = NormalUser_ROLE_ID,
                Name = "NormalUser",
                NormalizedName = "NORMALUSER",
            });

            modelBuilder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });


            modelBuilder.Entity<Property>()
                .HasOne(p => p.Agent) 
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.SetNull);


        }
    }
}


/*
 * Lidh Userin me Agjentin
 * Lidh Userin me Property
 * Bej manipulimin e database me LinQ
 * 
 * 
 * Ose Lidh Agjentin me Property ne lidhje 1 me n ==> 1 agjent ka sh propety dhe nje propety mund te shitet vetem nga nje agjent.
 */




// if we ever need to access the users CustomuserName in any view, we can do it by thisds methodd 
/*
 * @using Microsoft.AspNetCore.Identity
@using NestAlbania.Data

@inject UserManager<ApplicationUser> UserManager

 * @if (User.Identity.IsAuthenticated)
{
    var user = await UserManager.GetUserAsync(User);
    <span>@User.Identity.Name - @user.CustomUserName</span>
}
 */