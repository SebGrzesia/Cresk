using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cresk.Data;
using Cresk.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CreskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CreskContext") ?? throw new InvalidOperationException("Connection string 'CreskContext' not found.")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CreskContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/";
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    BasicCategoryData.Initialize(services);
}

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
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
