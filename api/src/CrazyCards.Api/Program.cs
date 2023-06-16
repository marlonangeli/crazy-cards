using CrazyCards.Application;
using CrazyCards.Infrastructure;
using CrazyCards.Presentation;
using CrazyCards.Security;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration, environment)
    .AddPresentation(configuration)
    .AddSecurity(configuration);

builder.Host.AddLogging();

var app = builder.Build();

app.UsePresentation()
    .UseSecurity();

app.Run();