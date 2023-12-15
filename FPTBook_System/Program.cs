using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FPTBook_System.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FPTBook_SystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FPTBook_SystemContext") ?? throw new InvalidOperationException("Connection string 'FPTBook_SystemContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(1); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=useraccounts}/{action=login}/{id?}");

app.Run();
