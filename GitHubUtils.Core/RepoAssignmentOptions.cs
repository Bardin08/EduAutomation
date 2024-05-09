namespace GitHubUtils.Core;

public record RepoAssignRequest(
    string Repo,
    string Org,
    int[] TeamIds,
    bool ReadOnlyAccess = true);
