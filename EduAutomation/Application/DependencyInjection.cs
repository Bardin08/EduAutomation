using EduAutomation.Application.GitHub.Services;
using EduAutomation.Application.Telegram.Formatters;
using EduAutomation.Application.Trello.Formatters;

namespace EduAutomation.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<ITrelloCardFormatter, MarkdownCardFormatter>();
        services.AddTransient<ITelegramMessageFormatter, MarkdownMessageFormatter>();

        services.AddScoped<IGitHubWebHookService, GitHubWebHookService>();

        return services;
    }
}
