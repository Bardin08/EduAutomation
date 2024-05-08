namespace EduAutomation.Domain.GitHub;

public record RepoCreatedEventPayload(
    Repo Repo,
    Org Org,
    User Sender
)
{
    public string Action => "created";
}
