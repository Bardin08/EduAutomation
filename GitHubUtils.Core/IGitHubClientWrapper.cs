using Octokit;

namespace GitHubUtils.Core;

public interface IGitHubClientWrapper
{
    Task<IEnumerable<Repository>> GetRepos(
        string organizationName, Func<Repository, bool> filter);

    Task<Repository> GetRepo(int repoId);

    Task<Team?> GetTeam(string organizationName, string teamName);

    Task<List<RepoAssignResponse>> GrantAccessToRepository(
        RepoAssignRequest request);
}
