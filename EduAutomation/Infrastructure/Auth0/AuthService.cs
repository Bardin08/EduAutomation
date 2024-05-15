using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using EduAutomation.Application.Auth0;
using EduAutomation.Domain.Auth0;
using Microsoft.Extensions.Options;

namespace EduAutomation.Infrastructure.Auth0;

public class AuthService(
    IAuthenticationApiClient authenticationApiClient,
    IOptions<Auth0Options> auth0Options) : IAuthService
{
    private readonly Auth0Options _auth0Options = auth0Options.Value;

    public async Task<string?> Login(LoginDto dto)
    {
        try
        {
            var auth0Response = await authenticationApiClient.GetTokenAsync(new ResourceOwnerTokenRequest
            {
                Username = dto.Login,
                Password = dto.Password,
                Realm = _auth0Options.Realm,

                Audience = _auth0Options.Audience,
                ClientId = _auth0Options.ClientId,
                ClientSecret = _auth0Options.ClientSecret,
                Scope = "openid"
            });

            return !string.IsNullOrEmpty(auth0Response?.AccessToken ?? null)
                ? auth0Response!.AccessToken : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<string?> Register(RegisterDto dto)
    {
        try
        {
            var username = $"{dto.FullName[0]}-{Guid.NewGuid().ToString()[..6]}";
            await authenticationApiClient.SignupUserAsync(new SignupUserRequest
            {
                Username = username,
                Nickname = username,
                Name = dto.FullName ,
                Password = dto.Password,
                Email = dto.Email,

                Connection = "edu-automation",
                ClientId = _auth0Options.ClientId
            });

            var jwtToken = await Login(new LoginDto(username, dto.Password));
            return jwtToken;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}
