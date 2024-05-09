using EduAutomation.Application.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace EduAutomation.Infrastructure.Telegram;

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
