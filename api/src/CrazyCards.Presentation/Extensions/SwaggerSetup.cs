using System.Reflection;
using CrazyCards.Security.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CrazyCards.Presentation.Extensions;

public static class SwaggerSetup
{
    public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var keycloakSettings = configuration.GetSection(KeycloakSettings.SectionName).Get<KeycloakSettings>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CrazyCards.Api",
                Version = "v1",
                Description = "API para CrazyCards",
                Contact = new OpenApiContact
                {
                    Name = "Marlon Angeli",
                    Email = "marlonangeli@duck.com",
                }
            });

            options.AddSecurityDefinition("OpenIdConnect", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = new Uri(Path.Combine(keycloakSettings!.AuthServerUrl, "realms",
                    keycloakSettings.Realm, ".well-known", "openid-configuration")),
                Description = "OpenID Connect",
                In = ParameterLocation.Header,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl =
                            new Uri(Path.Combine(keycloakSettings.AuthServerUrl, "realms", keycloakSettings.Realm,
                                "protocol/openid-connect/auth")),
                        TokenUrl = new Uri(Path.Combine(keycloakSettings.AuthServerUrl, "realms",
                            keycloakSettings.Realm, "protocol/openid-connect/token")),
                        RefreshUrl =
                            new Uri(Path.Combine(keycloakSettings.AuthServerUrl, "realms", keycloakSettings.Realm,
                                "protocol/openid-connect/token")),
                        Scopes = null
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "OpenIdConnect"
                        },
                        Type = SecuritySchemeType.OpenIdConnect,
                        OpenIdConnectUrl =
                            new Uri(Path.Combine(keycloakSettings!.AuthServerUrl, "realms",
                                keycloakSettings.Realm, ".well-known", "openid-configuration")),
                        Description = "OpenID Connect",
                        In = ParameterLocation.Header
                    },
                    new string[] { }
                }
            });
        });
    }

    public static void UseSwaggerUi(this WebApplication app)
    {
        var configuration = app.Services.GetRequiredService<IConfiguration>();
        var keycloakSettings = configuration.GetSection(KeycloakSettings.SectionName).Get<KeycloakSettings>();
        
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var apiVersionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();
            if (apiVersionProvider == null)
                throw new ArgumentException("API Versioning not registered.");

            foreach (var description in apiVersionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName);
            }

            options.RoutePrefix = "swagger";

            options.DocExpansion(DocExpansion.None);

            options.OAuthClientId(keycloakSettings.Resource);
            options.OAuthClientSecret(keycloakSettings.Credentials.Secret);
            options.OAuthAppName(keycloakSettings.Resource);

            options.OAuthUsePkce();
            options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            options.ConfigObject.AdditionalItems["tagsSorter"] = "alpha";
            options.DisplayRequestDuration();
            options.DisplayOperationId();
            options.EnablePersistAuthorization();
        });
    }
}