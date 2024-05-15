using EduAutomation.Domain.Auth0;

namespace EduAutomation.Application.Auth0;

public interface IAuthService
{
    Task<string?> Login(LoginDto dto);
    Task<string?> Register(RegisterDto dto);
}
