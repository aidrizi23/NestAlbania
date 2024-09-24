using Microsoft.EntityFrameworkCore;
using NestAlbania.Data; // Namespace where ApplicationDbContext is located
using NestAlbania.Repositories;
using NestAlbania.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure the ApplicationDbContext to use the same connection string as the admin panel
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS policy if your landing page is hosted separately
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add controllers (for Web API)
builder.Services.AddControllers();

// Add Swagger for API documentation (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<PropertyRepository, PropertyRepository>();
builder.Services.AddTransient<IPropertyService, PropertyService>();

builder.Services.AddScoped<AgentRepository, AgentRepository>();
builder.Services.AddTransient<IAgentService, AgentService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Enable CORS
app.UseCors("AllowAll");


app.MapControllers();

app.Run();