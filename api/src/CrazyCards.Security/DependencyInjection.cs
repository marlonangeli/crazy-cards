using CrazyCards.Security.Settings;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CrazyCards.Security;

public static class DependencyInjection
{
    public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        
        var keycloakSettings = configuration.GetSection(KeycloakSettings.SectionName).Get<KeycloakSettings>()!;
        services.AddKeycloakAdminHttpClient(new KeycloakAdminClientOptions
        {
            AuthServerUrl = keycloakSettings.AuthServerUrl,
            Credentials = new KeycloakClientInstallationCredentials
            {
                Secret = keycloakSettings.Credentials.Secret
            },
            Realm = keycloakSettings.Realm,
            Resource = keycloakSettings.Resource,
            RolesSource = RolesClaimTransformationSource.ResourceAccess,
            SslRequired = keycloakSettings.SslRequired,
            VerifyTokenAudience = keycloakSettings.VerifyTokenAudience
        });

        services.AddKeycloakAuthentication(configuration);
        services.AddKeycloakAuthorization(configuration);

        return services;
    }
    
    public static void AddLogging(this IHostBuilder host)
    {
        host.UseSerilog((context, configuration) => 
            configuration.ReadFrom.Configuration(context.Configuration));
    }
    
    public static void UseSecurity(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}