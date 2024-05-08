using EduAutomation.Domain.Trello;

namespace EduAutomation.Application.Formatters;

public interface ITrelloCardFormatter
{
    public TrelloCard GetCard<TEvent>(TEvent e, Dictionary<string, string> paramsBag);
}