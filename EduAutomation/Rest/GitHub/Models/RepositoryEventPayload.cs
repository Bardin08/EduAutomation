using System.Text.Json.Serialization;

namespace EduAutomation.Rest.GitHub.Models;

[method: JsonConstructor]
public class RepositoryEventPayload(
    string action,
    Repository repository,
    Organization organization,
    User sender)
{
    [JsonPropertyName("action")] public string Action { get; } = action;

    [JsonPropertyName("repository")] public Repository Repository { get; } = repository;

    [JsonPropertyName("organization")] public Organization Organization { get; } = organization;

    [JsonPropertyName("sender")] public User Sender { get; } = sender;
}

[method: JsonConstructor]
public class Organization(
    string login,
    int? id,
    string nodeId,
    string url,
    string htmlUrl,
    string description)
{
    [JsonPropertyName("login")] public string Login { get; } = login;

    [JsonPropertyName("id")] public int? Id { get; } = id;

    [JsonPropertyName("node_id")] public string NodeId { get; } = nodeId;
    [JsonPropertyName("url")] public string Url { get; } = url;

    [JsonPropertyName("html_url")] public string HtmlUrl { get; } = htmlUrl;

    [JsonPropertyName("description")] public string Description { get; } = description;
}

[method: JsonConstructor]
public class User(
    string login,
    int? id,
    string nodeId,
    string avatarUrl,
    string url,
    string htmlUrl,
    string type,
    bool? siteAdmin)
{
    [JsonPropertyName("login")] public string Login { get; } = login;

    [JsonPropertyName("id")] public int? Id { get; } = id;

    [JsonPropertyName("node_id")] public string NodeId { get; } = nodeId;

    [JsonPropertyName("avatar_url")] public string AvatarUrl { get; } = avatarUrl;

    [JsonPropertyName("url")] public string Url { get; } = url;

    [JsonPropertyName("html_url")] public string HtmlUrl { get; } = htmlUrl;

    [JsonPropertyName("type")] public string Type { get; } = type;

    [JsonPropertyName("site_admin")] public bool? SiteAdmin { get; } = siteAdmin;
}

[method: JsonConstructor]
public class Repository(
    int? id,
    string nodeId,
    string name,
    string fullName,
    bool? @private,
    User owner,
    string url,
    string htmlUrl,
    DateTime? createdAt,
    DateTime? updatedAt,
    DateTime? pushedAt,
    string cloneUrl,
    object homepage,
    string defaultBranch)
{
    [JsonPropertyName("id")] public int? Id { get; } = id;

    [JsonPropertyName("node_id")] public string NodeId { get; } = nodeId;

    [JsonPropertyName("name")] public string Name { get; } = name;

    [JsonPropertyName("full_name")] public string FullName { get; } = fullName;

    [JsonPropertyName("private")] public bool? Private { get; } = @private;

    [JsonPropertyName("owner")] public User Owner { get; } = owner;

    [JsonPropertyName("url")] public string Url { get; } = url;
    [JsonPropertyName("html_url")] public string HtmlUrl { get; } = htmlUrl;

    [JsonPropertyName("created_at")] public DateTime? CreatedAt { get; } = createdAt;

    [JsonPropertyName("updated_at")] public DateTime? UpdatedAt { get; } = updatedAt;

    [JsonPropertyName("pushed_at")] public DateTime? PushedAt { get; } = pushedAt;

    [JsonPropertyName("clone_url")] public string CloneUrl { get; } = cloneUrl;

    [JsonPropertyName("homepage")] public object Homepage { get; } = homepage;

    [JsonPropertyName("default_branch")] public string DefaultBranch { get; } = defaultBranch;
}
