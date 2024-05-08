using Octokit;

namespace GitHubUtils.Core;

public class GitHubClientWrapper(string githubToken) : IGitHubClientWrapper
{
    private readonly GitHubClient _gitHubClient = GetClient(githubToken);

    private static GitHubClient GetClient(string token) =>
        new(new ProductHeaderValue("KSE_UTILS"))
        {
            Credentials = new Credentials(token)
        };

    public async Task<IEnumerable<Repository>> GetRepos(
        string organizationName, Func<Repository, bool> filter)
    {
        var repos = await _gitHubClient.Repository.GetAllForOrg(organizationName);
        var filteredRepos = repos.Where(filter);
        return filteredRepos;
    }

    public Task<Repository> GetRepo(int repoId)
    {
        return _gitHubClient.Repository.Get(repoId);
    }

    public async Task<Team?> GetTeam(string organizationName, string teamName)
    {
        return await _gitHubClient.Organization.Team.GetByName(organizationName, teamName);
    }

    public async Task<List<RepoAssignResponse>> GrantAccessToRepository(
        RepoAssignRequest request)
    {
        var response = new List<RepoAssignResponse>();
        var mode = request.ReadOnlyAccess ? TeamPermissionLegacy.Pull : TeamPermissionLegacy.Admin;

        foreach (var teamId in request.TeamIds)
        {
            var isAdded = await _gitHubClient.Organization.Team.AddRepository(
                teamId, request.Org, request.Repo, new RepositoryPermissionRequest(mode));

            if (isAdded)
            {
                response.Add(new RepoAssignResponse(request.Repo, mode.ToString(), teamId));
            }
        }

        return response;
    }
}
