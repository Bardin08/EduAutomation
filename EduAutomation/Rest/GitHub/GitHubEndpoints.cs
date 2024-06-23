using EduAutomation.Application.GitHub;
using EduAutomation.Application.GitHub.Services;
using EduAutomation.Rest.GitHub.Mappers;
using EduAutomation.Rest.GitHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduAutomation.Rest.GitHub;

public static class GitHubEndpoints
{
    public static async ValueTask<IResult> RepositoryCreatedWebhook(
        RepositoryEventPayload payload,
        HttpRequest request,
        CancellationToken cancellationToken,
        [FromServices] IGitHubWebHookService webHookService)
    {
        if (request.Headers.TryGetValue("X-GitHub-Event", out var header) && header == "ping")
        {
            return Results.Ok("pong");
        }

        var action = payload.Action;
        if (action is not "created")
        {
            // don't process any repo related action except repo creation event.
            return Results.NoContent();
        }

        await webHookService.HandleRepoCreated(payload.ToDomainModel(), true, true);
        return Results.Ok();
    }

    public static async Task<IResult> GrantAccessToRepo(
        GrantRepoAccessRequest request,
        CancellationToken cancellationToken,
        [FromServices] IGitHubService githubService)
    {
        if (string.IsNullOrEmpty(request.Repository) ||
            string.IsNullOrEmpty(request.UserOrTeamName))
        {
            var errors = new Dictionary<string, string[]>
            {
                { "Invalid request model state", ["Soma mandatory fields are not complete"] }
            };

            return Results.ValidationProblem(errors);
        }

        var response = await githubService.AssignRepo(request.ToDomainModel());
        return Results.Ok(response);
    }
}
