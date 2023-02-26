using Microsoft.AspNetCore.Authorization;

namespace API.Mappings;

public static class FrontendMapping
{
    public static void MapFrontend(this WebApplication app)
    {
        app.MapGet("/", [AllowAnonymous] () => Results.Redirect("/index"));
        app.MapGet("/login", [AllowAnonymous] () => Results.Redirect("/login.html"));
        app.MapGet("/index", [AllowAnonymous] () => Results.Redirect("/index.html"));

        app.UseStaticFiles();
        app.MapFallbackToFile("/");
    }
}
