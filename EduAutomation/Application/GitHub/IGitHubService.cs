using EduAutomation.Rest.GitHub.Models;
using EduAutomation.Services;

namespace EduAutomation.Application.GitHub;

public interface IGitHubService
{
    Task<RepoAssignResponse> AssignRepoToAppropriateTeams(Repository repo);
}
