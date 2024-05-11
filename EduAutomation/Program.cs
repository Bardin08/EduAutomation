using EduAutomation.Application;
using EduAutomation.Infrastructure;
using EduAutomation.Rest.GitHub;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();

app.MapPost("/api/github-repo-webhook", GitHubEndpoints.RepositoryCreatedWebhook);

app.Run();
