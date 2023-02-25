using AutoMapper;
using Domain.Data;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Mappings;

public static class RouteMapping
{
    public static void MapRoutes(this WebApplication app)
    {
        app.MapGet("/api/v1/routes", [AllowAnonymous] ([FromServices] IRouteService service, [FromServices] IMapper mapper) =>
        {
            var routes = service.GetRoutes();
            var sorted = service.SortRoutes(routes);
            var dtos = sorted.Select(mapper.Map<RouteDto>).ToList();

            return Results.Ok(dtos);
        })
        .WithName("Routes")
        .WithOpenApi();
    }
}
