namespace EduAutomation.Domain.Auth0;

public record RegisterDto(
    string Login,
    string FullName,
    string Password,
    string Email);
