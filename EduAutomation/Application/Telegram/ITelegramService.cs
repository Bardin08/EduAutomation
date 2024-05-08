namespace EduAutomation.Services;

public interface ITelegramService
{
    Task PostToChannel(long chatId, string message);
}