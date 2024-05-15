namespace EduAutomation.Rest.Auth0.Models;

public record RegisterRequest(
    string Login,
    string FullName,
    string Password,
    string Email);
