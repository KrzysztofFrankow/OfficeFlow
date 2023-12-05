using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using OfficeFlow.Infrastructure.Extensions;
using OfficeFlow.Application.Extensions;
using OfficeFlow.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Login path
        options.AccessDeniedPath = "/Account/AccessDenied"; // Access denied path
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2880); // Expire time
    });

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Seed data
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<UserSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

app.MapGet("/", context =>
{
    // Check if the user is authenticated
    if (context.User.Identity?.IsAuthenticated == true)
    {
        // Redirect to the Home/Index if authenticated
        context.Response.Redirect("/Home/Index");
    }
    else
    {
        // Redirect to the Account/Login if not authenticated
        context.Response.Redirect("/Account/Login");
    }

    return Task.CompletedTask; // Dodajemy to, aby zwróciæ Task
});



app.Run();
