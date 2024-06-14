using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NestAlbania.Areas;
using NestAlbania.Data;
using NestAlbania.Repositories;
using NestAlbania.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();



#region Scoped
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<RoleRepository, RoleRepository>();
builder.Services.AddScoped<JobApplyRepository,  JobApplyRepository>();
builder.Services.AddScoped<CountryRepository, CountryRepository>();
builder.Services.AddScoped<PropertyRepository , PropertyRepository>();
#endregion

#region Transient
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRoleService, UserRoleService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IJobApplyService, JobApplyService>();
builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddTransient<ICountryService, CountryService>();
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