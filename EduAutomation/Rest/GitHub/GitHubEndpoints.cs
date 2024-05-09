using EduAutomation.Application.GitHub;
using EduAutomation.Application.GitHub.Services;
using EduAutomation.Application.Telegram;
using EduAutomation.Application.Trello;
using EduAutomation.Rest.GitHub.Mappers;
using EduAutomation.Rest.GitHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduAutomation.Rest.GitHub;

public class GitHubEndpoints
{
    public static async ValueTask<IResult> RepositoryCreatedWebhook(
        RepositoryEventPayload payload,
        CancellationToken cancellationToken,
        [FromServices] IGitHubWebHookService webHookService)
    {
        await webHookService.HandleRepoCreated(payload.ToDomainModel(), true, true);
        return Results.Ok();
    }
}
