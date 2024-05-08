using EduAutomation.Domain.Trello;
using Manatee.Trello;

namespace EduAutomation.Services;

public interface ITrelloService
{
    Task<ICard> CreateTrelloCard(TrelloCard card, TrelloOptions options);
}