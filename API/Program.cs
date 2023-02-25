
using API.Handlers;
using API.Mappings;
using API.Services;
using Domain.Profiles;
using Domain.Services;
using MSAuth = Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{

    public static async Task Main(string[] args)
    {
        await InstallFiles();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<IAuthenticationService, HardcodedAuthenticationService>();
        builder.Services.AddTransient<IRouteService, RouteService>();

        builder.Services.AddAutoMapper(typeof(RouteMappingProfile));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "MyCorsPolicy",
                            policy =>
                            {
                                policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                            });
        });

        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseMySQL(builder.Configuration.GetConnectionString("Database") ?? "-"));

        builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<MSAuth.AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuthentication", null);
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("MyCorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapAuthentication();
        app.MapRoutes();
        app.MapFrontend();

        app.Run();
    }

    private static async Task InstallFiles()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var wwwrootPath = Path.Combine(basePath, "wwwroot");
        var appSettingsPath = Path.Combine(basePath, "appsettings.json");

        var assembly = typeof(Program).Assembly;

        if (!File.Exists(appSettingsPath))
        {
            var resourcePath = assembly.GetManifestResourceNames()
                .Where(rnn => rnn.Contains("appsettings.json"))
                .First();

            var fileName = resourcePath;
            var filePath = Path.Combine(basePath, fileName);
            var fileInfo = new FileInfo(filePath);
            using var stream = File.Create(filePath);
            using var resourceStream = assembly.GetManifestResourceStream(resourcePath);

            if (stream != null && resourceStream != null)
            {
                await resourceStream.CopyToAsync(stream);
            }
        }

        if (!Directory.Exists(wwwrootPath))
        {
            Directory.CreateDirectory(wwwrootPath);

            var resourcePaths = assembly.GetManifestResourceNames()
                .Where(rnn => rnn.Contains("wwwroot"))
                .ToList();

            foreach (var resourcePath in resourcePaths)
            {
                var fileName = resourcePath;
                var filePath = Path.Combine(basePath, fileName);
                var fileInfo = new FileInfo(filePath);
                fileInfo.Directory?.Create();
                using var stream = File.Create(filePath);
                using var resourceStream = assembly.GetManifestResourceStream(resourcePath);

                if (stream != null && resourceStream != null)
                {
                    await resourceStream.CopyToAsync(stream);
                }
            }
        };
    }
}