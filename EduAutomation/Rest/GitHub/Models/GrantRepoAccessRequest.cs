namespace EduAutomation.Rest.GitHub.Models;

/// <summary>
/// Represents a request to assign access to a user or team within a GitHub organization.
/// </summary>
/// <remarks>
/// Later, the ability to grant specific permission levels (e.g., admin access) will be added.
/// Currently, this is not implemented due to the absence of an authentication mechanism.
/// </remarks>
/// <param name="Organization">The name of the GitHub organization.</param>
/// <param name="Repository">The name of the GitHub repository.</param>
/// <param name="UserOrTeamName">The username (if `IsUser` is true) or the team name (if `IsUser` is false) to grant access to.</param>
/// <param name="IsUser">Indicates whether access is being granted to a user (`true`) or a team (`false`).</param>
public record GrantRepoAccessRequest(
    string Organization,
    string Repository,
    string UserOrTeamName,
    bool IsUser);
