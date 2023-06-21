using CrazyCards.Domain.Extension;
using CrazyCards.Presentation.Extensions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CrazyCards.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                var converters = JsonConverterSetup.GetConverters();
                foreach (var converter in converters)
                {
                    options.SerializerSettings.Converters.Add(converter);
                }
                options.SerializerSettings.Converters.Add(new ProblemDetailsConverter());
            });
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