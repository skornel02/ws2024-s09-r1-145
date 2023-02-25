using Microsoft.AspNetCore.Authorization;

namespace API.Mappings;

public static class FrontendMapping
{
    public static void MapFrontend(this WebApplication app)
    {
        app.MapGet("/", [AllowAnonymous] () => Results.Redirect("/secure"));
        app.MapGet("/login", [AllowAnonymous] () => Results.Redirect("/login.html"));
        app.MapGet("/secure", [AllowAnonymous] () => Results.Redirect("/secure.html"));

        app.UseStaticFiles();
        app.MapFallbackToFile("secure");
    }
}
