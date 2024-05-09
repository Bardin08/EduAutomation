namespace GitHubUtils.Core;

public record RepoAssignResponse(
    string RepositoryName,
    string Mode,
    int TeamId);
