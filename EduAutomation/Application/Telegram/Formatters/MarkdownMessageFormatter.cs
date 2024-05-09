using System.Text;
using EduAutomation.Domain.GitHub.Events;

namespace EduAutomation.Application.Telegram.Formatters;

public class MarkdownMessageFormatter : ITelegramMessageFormatter
{
    public string GetMessage<TEvent>(TEvent e, Dictionary<string, string> paramsBag)
    {
        ArgumentNullException.ThrowIfNull(e);

        if (e.GetType() == typeof(RepoCreated))
        {
            return GetRepoCreatedCard(e as RepoCreated, paramsBag);
        }

        throw new ArgumentException(
            $"{nameof(MarkdownMessageFormatter)} doesn't support {e.GetType().Name} events");
    }

    private string GetRepoCreatedCard(RepoCreated? repoCreated, Dictionary<string, string> paramsBag)
    {
        if (repoCreated == null)
        {
            throw new ArgumentNullException(nameof(repoCreated));
        }

        var builder = new StringBuilder();

        builder.AppendLine("**Repo Created**");
        builder.AppendLine($"* **Repository:** [{repoCreated.Repo.Name}]({repoCreated.Repo.Url})");
        builder.AppendLine($"* **Organization:** [{repoCreated.Org.Name}]({repoCreated.Org.Url})");
        builder.AppendLine($"* **Created By:** [{repoCreated.Sender.Login}]({repoCreated.Sender.Url})");
        builder.AppendLine($"* **Date:** {DateTime.Now.ToString("MMMM dd, yyyy")}");

        TryAddAssignees(builder, paramsBag);

        return builder.ToString();
    }

    private void TryAddAssignees(StringBuilder sb, Dictionary<string, string> paramsBag)
    {
        if (!paramsBag.TryGetValue("assignees", out var assigneesString))
        {
            return;
        }

        sb.AppendLine();
        var assignees = assigneesString.Split(",");

        foreach (var assignee in assignees)
        {
            sb.AppendLine($"- {assignee}");
        }
    }
}
