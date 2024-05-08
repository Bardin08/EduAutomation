using System.Text;
using Microsoft.AspNetCore.Mvc;
using TestApi.Rest.GitHub.Models;
using TestApi.Services;

namespace TestApi.Rest.GitHub;

public class GitHubEndpoints
{
    public static async ValueTask<IResult> RepositoryCreatedWebhook(
        RepositoryEventPayload payload,
        CancellationToken cancellationToken,
        [FromServices] IGitHubService github,
        [FromServices] ITrelloService trello,
        [FromServices] ITelegramService telegramService,
        [FromServices] ILogger<GitHubEndpoints> logger)
    {
        var response = await github.AssignRepoToAppropriateTeams(payload.Repository);

        var message = MarkdownMessageGenerator.GenerateTicketMessage(payload, response);

        await Task.WhenAll(
            trello.CreateTrelloCard("New Repository Created!", message),
            telegramService.PostToChannel(-1002017960385, message)
        );

        return Results.Ok();
    }
}
