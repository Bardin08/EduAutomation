namespace EduAutomation.Domain.GitHub;

public record GrantRepoAccess(
    string Organization,
    string Repository,
    string UserOrTeamName,
    bool IsUser);
