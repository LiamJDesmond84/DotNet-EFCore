using DotNet_EFCore.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DBInfo:ConnectionString") ?? throw new InvalidOperationException("Connection string 'DBInfo:ConnectionString' not found.");
builder.Services.AddDbContext<MyContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//builder.Services.AddDbContext<MyContext>(options => options.UseMySql("DBInfo:ConnectionString", ServerVersion.AutoDetect("DBInfo:ConnectionString")));
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();