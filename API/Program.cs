
using API.Mappings;
using API.Services;
using Domain.Profiles;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
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

        app.MapAuthentication();
        app.MapRoutes();

        app.Run();
    }
}