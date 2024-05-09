namespace EduAutomation.Infrastructure.GitHub;

public record RepoAssignResponse(string Repo, Dictionary<string, string> Teams);
