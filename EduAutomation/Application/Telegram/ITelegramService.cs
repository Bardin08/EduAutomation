namespace EduAutomation.Application.Telegram;

public interface ITelegramService
{
    Task PostToChannel(long chatId, string message);
}
