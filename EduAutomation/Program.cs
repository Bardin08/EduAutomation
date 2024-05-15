using EduAutomation.Application;
using EduAutomation.Infrastructure;
using EduAutomation.Rest.Auth0;
using EduAutomation.Rest.GitHub;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();

app.MapPost("/api/github-repo-webhook", GitHubEndpoints.RepositoryCreatedWebhook);

app.MapPost("/api/auth/login", AuthEndpoints.Login);
app.MapPost("/api/auth/register", AuthEndpoints.Register);

app.Run();
