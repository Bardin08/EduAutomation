namespace EduAutomation.Domain.GitHub;

public record Repo(
    int? Id,
    string Name,
    string FullName,
    string Url,
    string CloneUrl,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    DateTimeOffset PushedAt,
    User Owner
);
