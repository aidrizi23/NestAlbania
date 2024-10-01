using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

using NestAlbania.Data;using Microsoft.EntityFrameworkCore;
using NestAlbania.Areas;
using NestAlbania.Repositories;
using NestAlbania.Services;
using NestAlbania.Hub;

using NestAlbania.Services.Extensions;
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
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
await Task.Run(() =>
{
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
stopwatch.Stop();
Console.WriteLine($"Application started in {stopwatch.ElapsedMilliseconds}ms");
await app.RunAsync();

