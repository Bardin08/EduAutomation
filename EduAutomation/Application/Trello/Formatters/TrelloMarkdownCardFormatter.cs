using System.Text;
using EduAutomation.Domain.GitHub.Events;
using EduAutomation.Domain.Trello;

namespace EduAutomation.Application.Trello.Formatters;

public class MarkdownCardFormatter : ITrelloCardFormatter
{
    public TrelloCard GetCard<TEvent>(TEvent e, Dictionary<string, string> paramsBag)
    {
        ArgumentNullException.ThrowIfNull(e);

        if (e.GetType() == typeof(RepoCreated))
        {
            return GetRepoCreatedCard(e as RepoCreated, paramsBag);
        }

        throw new ArgumentException(
            $"{nameof(MarkdownCardFormatter)} doesn't support {e.GetType().Name} events");
    }

    private TrelloCard GetRepoCreatedCard(RepoCreated? e, Dictionary<string, string> paramsBag)
    {
        ArgumentNullException.ThrowIfNull(e);

        var builder = new StringBuilder();

        builder.AppendLine($"## Repository Creation Event: [{e.Repo.Name}]({e.Repo.Url})");
        builder.AppendLine();

        TryAddTeachingAssistant(builder, paramsBag);
        TryAddMentor(builder, paramsBag);

        builder.AppendLine();

        // Repository Details
        builder.AppendLine("**### Repository Details**");
        builder.AppendLine($"**Full Name:** {e.Repo.Name}");
        builder.AppendLine($"**Created At:** {e.Repo.CreatedAt}");
        builder.AppendLine();

        // Organization and Creator
        builder.AppendLine("**Organization and Creator**");
        builder.AppendLine($"- **Organization:** [{e.Org.Name}](https://github.com/{e.Org.Name})");
        builder.AppendLine($"- **Creator:** [{NormalizedSenderLogin(e)}]({e.Sender.Url})");
        builder.AppendLine();

        // Assignment
        builder.AppendLine("### **Assignment**");
        builder.AppendLine("**Deadlines:**");
        builder.AppendLine("\tSoft Deadline: TBA");
        builder.AppendLine("\tHard Deadline: TBA");

        var title = GetTicketTitle(e.Repo.Name);
        var description = builder.ToString();

        return new TrelloCard(title, description);
    }

    private string GetTicketTitle(string repoName)
    {
        var chunks = repoName.Split('-');
        var workType = chunks[0].ToLower();
        var userName = chunks[^1];

        var capitalizedWorkType = char.ToUpper(workType[0]) + workType[1..].ToLower();

        return $"{capitalizedWorkType} for {userName}";
    }

    private static void TryAddTeachingAssistant(StringBuilder builder, Dictionary<string, string> paramsBag)
    {
        if (paramsBag.TryGetValue("teaching-assistant", out var teachingAssistantName))
        {
            builder.AppendLine($"**Teaching Assistant:** {teachingAssistantName}");
        }
    }

    private static void TryAddMentor(StringBuilder builder, Dictionary<string, string> paramsBag)
    {
        var mentorRequired = paramsBag.TryGetValue("mentor", out var mentorName);
        if (mentorRequired)
        {
            builder.AppendLine($"**Mentor:** {mentorName}");
        }
    }

    private string NormalizedSenderLogin(RepoCreated? e)
    {
        return e.Sender.Login.Replace("[bot]", "_bot");
    }
}
