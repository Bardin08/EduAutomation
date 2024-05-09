using GitHubUtils.Core;

const string githubToken = "pat_token";
const string organizationName = "org_name";
const string teamName = "team_name";
const string repositoryPrefix = "repo_prefix";

var githubClient = new GitHubClientWrapper(githubToken);

var team = await githubClient.GetTeam(organizationName, teamName);
var filteredRepos = await githubClient.GetRepos(organizationName,
    r => r.Name.StartsWith(repositoryPrefix, StringComparison.OrdinalIgnoreCase));

foreach (var repo in filteredRepos)
{
    var request = new RepoAssignRequest(repo.Name, organizationName, [team.Id]);
    var response = await githubClient.GrantAccessToRepository(request);
    Console.WriteLine("Access granted to: " + response.First().RepositoryName);
}
