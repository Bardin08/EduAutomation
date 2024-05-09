namespace EduAutomation.Infrastructure.Telegram;

public record TelegramOptions
{
    public const string SectionName = "Telegram";
    public long ChannelId { get; init; }
}
