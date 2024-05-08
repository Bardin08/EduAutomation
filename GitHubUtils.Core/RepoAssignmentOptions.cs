using Octokit;

namespace GitHubUtils;

internal record RepoAssignmentOptions(
    Team Team,
    string OrganizationName,
    bool ReadOnlyAccess = true);
