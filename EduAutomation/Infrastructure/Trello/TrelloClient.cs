using EduAutomation.Application.Trello;
using EduAutomation.Domain.Trello;
using Manatee.Trello;

namespace EduAutomation.Infrastructure.Trello;

public class TrelloClient : ITrelloService
{
    public async Task<ICard> CreateTrelloCard(TrelloCard card, TrelloOptions options)
    {
        var factory = new TrelloFactory();
        var board = factory.Board(options.BoardId);
        await board.Refresh();

        var list = board.Lists.First(l => l.Id == options.ListId);
        return await list.Cards.Add(card.Title, description: card.Description);
    }
}
