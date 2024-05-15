using Auth0Net.DependencyInjection;
using EduAutomation.Application.GitHub;
using EduAutomation.Application.Telegram;
using EduAutomation.Application.Trello;
using EduAutomation.Infrastructure.Auth0;
using EduAutomation.Infrastructure.GitHub;
using EduAutomation.Infrastructure.Telegram;
using EduAutomation.Infrastructure.Trello;
using GitHubUtils.Core;
using Manatee.Trello;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Telegram.Bot;

namespace EduAutomation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuth0(configuration);

        services.AddTrello(configuration);
        services.AddGitHub(configuration);
        services.AddTelegram(configuration);

        return services;
    }

    private static void AddAuth0(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Auth0Options>(configuration.GetSection(Auth0Options.SectionName));
        services.AddAuth0AuthenticationClient(c =>
        {
            c.Domain = configuration["Auth0:Domain"]!;
            c.ClientId = configuration["Auth0:ClientId"]!;
            c.ClientSecret = configuration["Auth0:ClientSecret"]!;
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = configuration["Auth0:Authority"];
            options.Audience = configuration["Auth0:Audience"];
        });
    }

    private static void AddTrello(this IServiceCollection services, IConfiguration configuration)
    {
        TrelloAuthorization.Default.AppKey = configuration[TrelloOptions.SectionName + ":AppKey"];
        TrelloAuthorization.Default.UserToken = configuration[TrelloOptions.SectionName + ":UserToken"];

        services.Configure<TrelloOptions>(configuration.GetSection(TrelloOptions.SectionName));
        services.AddScoped<ITrelloService, TrelloClient>();
    }

    private static void AddGitHub(this IServiceCollection services, IConfiguration configuration)
    {
        var patToken = configuration["GitHub:PatToken"];
        ArgumentException.ThrowIfNullOrEmpty(patToken);

        services.AddScoped<IGitHubClientWrapper>(_ => new GitHubClientWrapper(patToken));
        services.AddScoped<IGitHubService, GitHubService>();
    }

    private static void AddTelegram(this IServiceCollection services, IConfiguration configuration)
    {
        var patToken = configuration["Telegram:BotToken"];
        ArgumentException.ThrowIfNullOrEmpty(patToken);

        services.Configure<TelegramOptions>(
            configuration.GetSection(TelegramOptions.SectionName));

        services.AddScoped<ITelegramBotClient>(_ => new TelegramBotClient(patToken));
        services.AddScoped<ITelegramService, TelegramService>();
    }
}
