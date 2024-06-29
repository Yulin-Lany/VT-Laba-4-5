using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stseniayeva.UI.Data;
using Stseniayeva.UI.Services;
using System.Security.Claims;
using Stseniayeva.UI.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Stseniayeva.Blazor.Services;
using Stseniayeva.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddHttpClient<IProductService, ApiProductService>(opt
=> opt.BaseAddress = new Uri("https://localhost:7002/api/tovary/"));
builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(opt
=> opt.BaseAddress = new
Uri("https://localhost:7002/api/categories/"));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
builder.Services.AddScoped<IProductService, MemoryProductService>();

builder.Services.AddHttpContextAccessor();


builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("admin", p =>
    p.RequireClaim(ClaimTypes.Role, "admin"));
});
//builder.Services.AddSingleton<IEmailSender, NoOpEmailSender>();



builder.Services.AddControllersWithViews();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
app.UseSession();

builder.Services
.AddHttpClient<IProductService<Moto>, Stseniayeva.Blazor.Services.ApiProductService>(c =>
c.BaseAddress = new Uri("https://localhost:7002/api/dishes"));