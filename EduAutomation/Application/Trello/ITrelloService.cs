using EduAutomation.Domain.Trello;
using EduAutomation.Infrastructure.Trello;
using Manatee.Trello;

namespace EduAutomation.Application.Trello;

public interface ITrelloService
{
    Task<ICard> CreateTrelloCard(TrelloCard card, TrelloOptions options);
}
