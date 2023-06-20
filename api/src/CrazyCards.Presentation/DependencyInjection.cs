using CrazyCards.Presentation.Extensions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrazyCards.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddVersioning();
        services.AddApiProblemDetails();
        services.AddSwagger(configuration);

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseSwaggerUi();
        app.UseProblemDetails();
        app.MapControllers();

        return app;
    }
}