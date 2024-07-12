using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NestAlbania.Areas;
using NestAlbania.Data;
using NestAlbania.Repositories;
using NestAlbania.Services;
using NestAlbania.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
 void ConfigureServices(IServiceCollection services)
{
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = "YOUR_GOOGLE_CLIENT_ID";
        options.ClientSecret = "YOUR_GOOGLE_CLIENT_SECRET";
    });

    services.AddControllersWithViews();
}

 void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}


#region Scoped
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<RoleRepository, RoleRepository>();
builder.Services.AddScoped<JobApplyRepository,  JobApplyRepository>();
builder.Services.AddScoped<CountryRepository, CountryRepository>();
builder.Services.AddScoped<PropertyRepository , PropertyRepository>();
builder.Services.AddScoped<AgentRepository, AgentRepository>();
#endregion

#region Transient
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IJobApplyService, JobApplyService>();
builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<IAgentService, AgentService>();
builder.Services.AddTransient<IFileHandlerService, FileHandlerService>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();