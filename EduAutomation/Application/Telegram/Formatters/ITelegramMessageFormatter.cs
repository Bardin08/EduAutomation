namespace EduAutomation.Application.Telegram.Formatters;

public interface ITelegramMessageFormatter
{
    public string GetMessage<TEvent>(TEvent e, Dictionary<string, string> paramsBag);
}
