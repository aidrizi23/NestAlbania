//
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using NestAlbania.Areas;
// using NestAlbania.Data;
// using NestAlbania.Repositories;
// using NestAlbania.Services;
// using NestAlbania.Services.Extensions;
// using Microsoft.AspNetCore.SignalR;
// using NestAlbania.Hub;
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(connectionString));
// builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//
// builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddDefaultTokenProviders();
//
// // SignalR Service
// builder.Services.AddSignalR();
//
// // remove all the password requirements
// builder.Services.Configure<IdentityOptions>(options =>
// {
//     options.Password.RequireDigit = false;
//     options.Password.RequireLowercase = false;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequireUppercase = false;
//     options.Password.RequiredLength = 6;
//     options.Password.RequiredUniqueChars = 1;
// });
// //lockout options
// builder.Services.Configure<IdentityOptions>(options =>
// {
//     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//     options.Lockout.MaxFailedAccessAttempts = 5;
//     options.Lockout.AllowedForNewUsers = true;
// });
//
// builder.Services.AddControllersWithViews();
//
// builder.Services.AddResponseCaching();
// builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//
//
//
// #region Scoped
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<UserRoleRepository, UserRoleRepository>();
// builder.Services.AddScoped<RoleRepository, RoleRepository>();
// builder.Services.AddScoped<PropertyRepository, PropertyRepository>();
// builder.Services.AddScoped<AgentRepository, AgentRepository>();
// builder.Services.AddScoped<NotificationRepository, NotificationRepository>();
// #endregion
//
// #region Transient
// builder.Services.AddTransient<IUserService, UserService>();
// builder.Services.AddTransient<IUserRoleService, UserRoleService>();
// builder.Services.AddTransient<IRoleService, RoleService>();
// builder.Services.AddTransient<IPropertyService, PropertyService>();
// builder.Services.AddTransient<IAgentService, AgentService>();
// builder.Services.AddTransient<IFileHandlerService, FileHandlerService>();
// builder.Services.AddTransient<INotificationService, NotificationService>();
// #endregion
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseMigrationsEndPoint();
// }
// else
// {
//     app.UseExceptionHandler("/Home/Error");
//     app.UseHsts();
// }
//
// app.UseHttpsRedirection();
// app.UseStaticFiles();
//
// app.UseRouting();
//
// app.UseAuthentication();
// app.UseAuthorization();
// app.UseResponseCaching();
//
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
//
// // Add SignalR endpoint
// app.MapHub<NotificationHub>("/notificationHub");
//
// app.Use(async (context, next) =>
// {
//     if (context.Request.Path == "/")
//     {
//         context.Response.Redirect("/account/login");
//         return;
//     }
//     await next();
// });
//
// app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NestAlbania.Areas;
using NestAlbania.Data;
using NestAlbania.Repositories;
using NestAlbania.Services;
using NestAlbania.Hub;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using NestAlbania.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions => 
    {
        sqlServerOptions.EnableRetryOnFailure();
        sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    })
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => 
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();

// Scoped services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserRoleRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<PropertyRepository>();
builder.Services.AddScoped<AgentRepository>();
builder.Services.AddScoped<NotificationRepository>();


// Transient services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddTransient<IAgentService, AgentService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddTransient<IFileHandlerService, FileHandlerService>();

var app = builder.Build();

// Warmup middleware
bool _warmupDone = false;
app.Use(async (context, next) =>
{
    if (!_warmupDone)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        await dbContext.EnsureSeedDataAsync();
        await dbContext.ApplicationUsers.FirstOrDefaultAsync();
        _warmupDone = true;
    }
    await next();
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection()
   .UseStaticFiles()
   .UseRouting()
   .UseResponseCaching()
   .UseAuthentication()
   .UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificationHub>("/notificationHub");

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/account/login");
        return;
    }
    await next();
});

await app.RunAsync();