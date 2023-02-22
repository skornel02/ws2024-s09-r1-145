
using API.Mappings;
using API.Services;
using Domain.Profiles;
using Domain.Services;
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

        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseMySQL(builder.Configuration.GetConnectionString("Database") ?? "-"));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.MapAuthentication();

        app.MapFallbackToFile("index.html");

        app.Run();
    }

    private static async Task InstallFiles()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var wwwrootPath = Path.Combine(basePath, "wwwroot");

        if (!Directory.Exists(wwwrootPath))
        {
            var assembly = typeof(Program).Assembly;
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