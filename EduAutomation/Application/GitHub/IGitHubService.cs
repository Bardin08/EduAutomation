using EduAutomation.Domain.GitHub;
using EduAutomation.Infrastructure.GitHub;

namespace EduAutomation.Application.GitHub;

public interface IGitHubService
{
    Task<RepoAssignResponse> AssignRepoToAppropriateTeams(Repo repo);
    Task<RepoAssignResponse> AssignRepo(GrantRepoAccess req);
}
