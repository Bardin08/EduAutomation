using EduAutomation.Domain.Auth0;
using EduAutomation.Rest.Auth0.Models;

namespace EduAutomation.Rest.Auth0.Mappers;

public static class RegisterRequestMapper
{
    public static RegisterDto ToDomainModel(this RegisterRequest r)
    {
        return new RegisterDto(r.Login, r.FullName, r.Password, r.Email);
    }
}
