using EduAutomation.Application.Auth0;
using EduAutomation.Rest.Auth0.Mappers;
using EduAutomation.Rest.Auth0.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduAutomation.Rest.Auth0;

public static class AuthEndpoints
{
    public static async Task<IResult> Login(
        LoginRequest payload,
        CancellationToken cancellationToken,
        [FromServices] IAuthService authService)
    {
        var loginDto = payload.ToDomainModel();
        var jwtToken = await authService.Login(loginDto);

        return !string.IsNullOrEmpty(jwtToken)
            ? Results.Ok(jwtToken)
            : Results.Unauthorized();
    }

    public static async Task<IResult> Register(
        RegisterRequest payload,
        CancellationToken cancellationToken,
        [FromServices] IAuthService authService)
    {
        var registerDto = payload.ToDomainModel();
        var jwtToken = await authService.Register(registerDto);

        return !string.IsNullOrEmpty(jwtToken)
            ? Results.Ok(jwtToken)
            : Results.Unauthorized();
    }
}
