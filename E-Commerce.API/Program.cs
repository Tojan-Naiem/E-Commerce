using E_Commerce.BLL.Repository;
using E_Commerce.BLL.Service.Classes;
using E_Commerce.DAL.Model;
using E_Commerce.DAL.Repository;
using E_Commerce.DAL.Repository.Classes;
using E_Commerce.DAL.Repository.Interfaces;
using E_Commerce.DAL.Utils;
using E_Commerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<BrandRepository>();
builder.Services.AddScoped<BrandService>();
builder.Services.AddScoped<IBrandRepository>();
builder.Services.AddScoped<ICategoryRepository>();

builder.Services.AddScoped<ISeedData,SeedData>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();




builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");




// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// add db context configuration
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
  
    ));
var app = builder.Build();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
var scope = app.Services.CreateScope();
var objectOfSeedData = scope.ServiceProvider.GetRequiredService<ISeedData>();
await objectOfSeedData.DataSeedingAsync();
await objectOfSeedData.IdentityDataSeedingAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
