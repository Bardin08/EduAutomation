using EduAutomation.Domain.GitHub;
using EduAutomation.Rest.GitHub.Models;

namespace EduAutomation.Rest.GitHub.Mappers;

public static class GrantRepoAccessMapper
{
    public static GrantRepoAccess ToDomainModel(this GrantRepoAccessRequest r)
    {
        return new GrantRepoAccess(r.Organization, r.Repository, r.UserOrTeamName, r.IsUser);
    }
}
