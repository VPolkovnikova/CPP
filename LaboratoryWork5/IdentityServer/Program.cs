using IdentityServer;
using IdentityServer.Data;
using IdentityServer.Data.Entities;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var environment = builder.Environment;

services.AddDbContextPool<IdentityServerDbContext>(builder =>
{
    var dataSource = Path.Join(environment.ContentRootPath, "Database.db");
    builder.UseSqlite($"Data source={dataSource}");
});
services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityServerDbContext>();
services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory>();
services.AddIdentityServer()
    .AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddAspNetIdentity<User>();
services.AddControllersWithViews();


var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    await using var context = scope.ServiceProvider.GetRequiredService<IdentityServerDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.UseStaticFiles();
app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.Always
});
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllerRoute(
    "Default",
    "{Controller=Home}/{Action=Index}"
);

app.Run();