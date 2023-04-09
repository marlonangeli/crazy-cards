using System.Security.Claims;
using CrazyCards.Security.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CrazyCards.Security.Extensions;

public static class OidcExtension
{
    public static void AddOidcAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var oidcSettings = configuration.GetSection(OidcSettings.SectionName).Get<OidcSettings>();
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OpenIdConnectDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(options =>
            {
                options.Authority = oidcSettings.Authority;
                options.ClientId = oidcSettings.ClientId;
                options.ClientSecret = oidcSettings.ClientSecret;
                options.RequireHttpsMetadata = false;
                options.ResponseType = "code id_token";
                options.RemoteSignOutPath = new PathString("/SignOut");
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,
                    ValidIssuer = oidcSettings.Authority,
                };
            });
    }

    public static void ConfigureOidcSwagger(this SwaggerGenOptions options, IConfiguration configuration)
    {
        var oidcSettings = configuration.GetSection(OidcSettings.SectionName).Get<OidcSettings>();
        var oidcUrl = Path.Combine(oidcSettings.Authority, "protocol/openid-connect");

        options.AddSecurityDefinition(OpenIdConnectDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OpenIdConnect,
            OpenIdConnectUrl = new Uri(Path.Combine(oidcSettings.Authority, ".well-known/openid-configuration")),
            Description = "OpenID Connect",
            In = ParameterLocation.Header,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(Path.Combine(oidcUrl, "auth")),
                    TokenUrl = new Uri(Path.Combine(oidcUrl, "token")),
                    RefreshUrl = new Uri(Path.Combine(oidcUrl, "token")),
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
                        Id = OpenIdConnectDefaults.AuthenticationScheme
                    }
                },
                new string[] { }
            }
        });
    }

    public static void ConfigureOidcSwaggerUI(this SwaggerUIOptions options, IConfiguration configuration)
    {
        var oidcSettings = configuration.GetSection(OidcSettings.SectionName).Get<OidcSettings>();
        options.OAuthClientId(oidcSettings.ClientId);
        options.OAuthClientSecret(oidcSettings.ClientSecret);
        options.OAuthAppName(oidcSettings.ClientId);

        options.OAuthUsePkce();
        options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        options.ConfigObject.AdditionalItems["tagsSorter"] = "alpha";
        options.DisplayRequestDuration();
        options.DocExpansion(DocExpansion.None);
        options.DisplayOperationId();
        options.EnablePersistAuthorization();
    }
}