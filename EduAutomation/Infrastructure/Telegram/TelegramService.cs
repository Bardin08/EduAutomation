using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace EduAutomation.Services;

public interface ITelegramService
{
    Task PostToChannel(long chatId, string message);
}

public class TelegramService(ITelegramBotClient telegramBotClient) : ITelegramService
{
    public async Task PostToChannel(long chatId, string message)
    {
        await telegramBotClient.SendTextMessageAsync(
            chatId,
            message,
            parseMode: ParseMode.Markdown,
            disableWebPagePreview: true);
    }
}
