using Microsoft.EntityFrameworkCore;
using westcoast_cars.web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddDbContext<ItemsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<ItemsContext>();
    await context.Database.MigrateAsync();
    await SeedData.LoadItems(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
