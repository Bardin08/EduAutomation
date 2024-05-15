using EduAutomation.Application.Auth0;
using EduAutomation.Application.GitHub.Services;
using EduAutomation.Application.Telegram.Formatters;
using EduAutomation.Application.Trello.Formatters;
using EduAutomation.Infrastructure.Auth0;

namespace EduAutomation.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        services.AddTransient<ITrelloCardFormatter, MarkdownCardFormatter>();
        services.AddTransient<ITelegramMessageFormatter, MarkdownMessageFormatter>();

        services.AddScoped<IGitHubWebHookService, GitHubWebHookService>();

        return services;
    }
}
