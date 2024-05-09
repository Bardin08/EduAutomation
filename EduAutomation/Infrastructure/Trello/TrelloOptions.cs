namespace EduAutomation.Infrastructure.Trello;

public record TrelloOptions
{
    public const string SectionName = "Trello";
    public required string BoardId { get; set; }
    public required string ListId { get; set; }
}
