namespace EduAutomation.Domain.GitHub.Events;

public record RepoCreated(
    Repo Repo,
    Org Org,
    User Sender
)
{
    public string Action => "created";
}
