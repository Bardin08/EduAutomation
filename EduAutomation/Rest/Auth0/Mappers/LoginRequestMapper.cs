using EduAutomation.Domain.Auth0;
using EduAutomation.Rest.Auth0.Models;

namespace EduAutomation.Rest.Auth0.Mappers;

public static class LoginRequestMapper
{
    public static LoginDto ToDomainModel(this LoginRequest r)
    {
        return new LoginDto(r.Login, r.Password);
    }
}
