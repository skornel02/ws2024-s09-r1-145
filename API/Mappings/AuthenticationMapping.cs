using Domain.Data;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Mappings
{
    public static class AuthenticationMapping
    {
        public static void MapAuthentication(this WebApplication app)
        {
            app.MapPost("/api/v1/login", ([FromBody] AuthenticationRequest request, [FromServices] IAuthenticationService authService) =>
            {
                if (authService.ValidatePassword(request.Username, request.Password))
                {
                    return Results.NoContent();
                }
                else
                {
                    return Results.Json(
                        new ErrorResponse()
                        {
                            Error = "Wrong username or password"
                        },
                        statusCode: (int)HttpStatusCode.Unauthorized);
                }
            })
            .WithName("Authenticate")
            .WithOpenApi();
        }
    }
}
