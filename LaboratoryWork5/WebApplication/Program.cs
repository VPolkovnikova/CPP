using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using WebApplication.Identity;


var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        options.Authority = "http://localhost:5001";
        options.ClientId = "web-application";
        options.ClientSecret = "web-application-secret";
        options.ResponseType = "code";

        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClaimActions.MapUniqueJsonKey(UserClaimTypes.FullName, UserClaimTypes.FullName);
        options.ClaimActions.MapUniqueJsonKey(UserClaimTypes.PhoneNumber, UserClaimTypes.PhoneNumber);

        options.RequireHttpsMetadata = false;
        options.SaveTokens = true;
    });
services.AddControllersWithViews();


var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    "LaboratoryWork",
    "LaboratoryWork/{LaboratoryWorkNumber:int}",
    new
    {
        Controller = "LaboratoryWork",
        Action = "Index"
    }
);
app.MapControllerRoute(
    "Default",
    "{Controller=Home}/{Action=Index}"
);

app.Run();