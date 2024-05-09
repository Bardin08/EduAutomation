using EduAutomation.Application.Telegram;
using EduAutomation.Application.Telegram.Formatters;
using EduAutomation.Application.Trello;
using EduAutomation.Application.Trello.Formatters;
using EduAutomation.Domain.GitHub.Events;
using EduAutomation.Infrastructure.Telegram;
using EduAutomation.Infrastructure.Trello;
using Microsoft.Extensions.Options;

namespace EduAutomation.Application.GitHub.Services;

public interface IGitHubWebHookService
{
    Task HandleRepoCreated(RepoCreated e, bool createTicket, bool sendTgMessage);
}

public class GitHubWebHookService(
    IGitHubService github,

    IOptions<TrelloOptions> trelloOptions,
    ITrelloCardFormatter trelloCardFormatter,
    ITrelloService trello,

    IOptions<TelegramOptions> telegramOptions,
    ITelegramMessageFormatter telegramMessageFormatter,
    ITelegramService telegramService
) : IGitHubWebHookService
{
    public async Task HandleRepoCreated(
        RepoCreated e, bool createTicket, bool sendTgMessage)
    {
        var response = await github.AssignRepoToAppropriateTeams(e.Repo);

        var paramsBag = new Dictionary<string, string>
        {
            { "teaching-assistant", "TBA" },
            { "mentor", "Tymofii Bezverkhyi (Mentor Manager) - @___" },
            { "assignees", "Vladyslav Bardin (Course Instructor) - @bardin08,TBA" }
        };


        var tgTask = GetTelegramTask(sendTgMessage, e, paramsBag);
        var trelloTask = GetTrelloTask(createTicket, e, paramsBag);
        await Task.WhenAll(tgTask, trelloTask);
    }

    private Task GetTelegramTask(
        bool sendTgMessage, RepoCreated e, Dictionary<string, string> paramsBag)
    {
        if (sendTgMessage)
        {
            var message = telegramMessageFormatter.GetMessage(e, paramsBag);
            return telegramService.PostToChannel(telegramOptions.Value.ChannelId, message);
        }

        return Task.CompletedTask;
    }

    private Task GetTrelloTask(
        bool createCard, RepoCreated e, Dictionary<string, string> paramsBag)
    {
        if (createCard)
        {
            var card = trelloCardFormatter.GetCard(e, paramsBag);
            return trello.CreateTrelloCard(card, trelloOptions.Value);
        }

        return Task.CompletedTask;
    }
}
