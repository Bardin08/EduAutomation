﻿using EduAutomation.Application.GitHub;
using EduAutomation.Domain.GitHub;
using GitHubUtils.Core;

namespace EduAutomation.Infrastructure.GitHub;

internal class GitHubService(
    IGitHubClientWrapper github,
    ILogger<GitHubService> logger) : IGitHubService
{
    public async Task<RepoAssignResponse> AssignRepoToAppropriateTeams(Repo repo)
    {
        var orgName = repo.Owner.Login;

        var teamNames = GetRelatedTeams(repo.Name);
        var teamInfos = await GetTeamInfos(teamNames, orgName);
        var teamIds = teamInfos.Keys.ToArray();

        var responses = await github.GrantAccessToRepository(
            new RepoAssignRequest(repo.Name, repo.Owner.Login, teamIds));
        return MapResponses(teamInfos, responses);
    }

    public async Task<RepoAssignResponse> AssignRepo(GrantRepoAccess req)
    {
        if (!req.IsUser)
        {
            var teamInfo = await GetTeamInfos([req.UserOrTeamName], req.Organization);
            var teamIds = teamInfo.Keys.ToArray();

            var responses = await github.GrantAccessToRepository(
                new RepoAssignRequest(req.Repository, req.Organization, teamIds));
            return MapResponses(teamInfo, responses);
        }

        throw new NotImplementedException("This flow is not implemented yet");
    }

    private RepoAssignResponse MapResponses(
        IReadOnlyDictionary<int, string> teamInfos,
        List<GitHubUtils.Core.RepoAssignResponse> responses)
    {
        var repoName = responses.First().RepositoryName;
        var teams = new Dictionary<string, string>();

        foreach (var response in responses)
        {
            var mode = response.Mode.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                ? "Full Access"
                : "Read Only";

            teams.Add(teamInfos[response.TeamId], mode);
        }

        return new RepoAssignResponse(repoName, teams);
    }

    private async Task<Dictionary<int, string>> GetTeamInfos(
        IEnumerable<string> teamNames, string orgName)
    {
        var infos = new Dictionary<int, string>();

        foreach (var name in teamNames)
        {
            var teamId = await GetTeamId(orgName, name);
            infos.Add(teamId, name);
        }

        return infos;
    }

    private IEnumerable<string> GetRelatedTeams(string repoName)
    {
        // TODO: Move to separated service. Add option to reflect this logic with YAML files.
        // Also add support for groups determine logic, and reports generating logic.
        var teams = new List<string> { "course-team" };

        if (repoName.StartsWith("hw", StringComparison.OrdinalIgnoreCase))
        {
            teams.Add("external-assistants");
        }

        return teams;
    }

    private async Task<int> GetTeamId(string orgName, string teamName)
    {
        return (await github.GetTeam(orgName, teamName))!.Id;
    }
}
