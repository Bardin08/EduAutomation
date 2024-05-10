using EduAutomation.Domain.GitHub;
using EduAutomation.Domain.GitHub.Events;
using EduAutomation.Rest.GitHub.Models;

namespace EduAutomation.Rest.GitHub.Mappers;

public static class RepoEventMapper
{
    public static RepoCreated ToDomainModel(this RepositoryEventPayload p)
    {
        return new RepoCreated(
            Repo: new Repo(
                Id: p.Repository.Id,
                Name: p.Repository.Name,
                FullName: p.Repository.FullName,
                Url: p.Repository.HtmlUrl,
                CloneUrl: p.Repository.CloneUrl,
                CreatedAt: p.Repository.CreatedAt ?? DateTimeOffset.MinValue,
                UpdatedAt: p.Repository.UpdatedAt ?? DateTimeOffset.MinValue,
                PushedAt: p.Repository.PushedAt ?? DateTimeOffset.MinValue,
                Owner: new EduAutomation.Domain.GitHub.User(
                    Id: p.Repository.Owner.Id,
                    Login: p.Repository.Owner.Login,
                    Url: p.Repository.Owner.HtmlUrl,
                    AvatarUrl: p.Repository.Owner.AvatarUrl
                )
            ),
            Org: new Org(
                Id: p.Organization.Id,
                Name: p.Organization.Login,
                Url: p.Organization.HtmlUrl
            ),
            Sender: new EduAutomation.Domain.GitHub.User(
                Id: p.Sender.Id,
                Login: p.Sender.Login,
                Url: p.Sender.HtmlUrl,
                AvatarUrl: p.Sender.AvatarUrl
            )
        );
    }
}
