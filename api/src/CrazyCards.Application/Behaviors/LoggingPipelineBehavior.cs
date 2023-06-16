using MediatR;
using Microsoft.Extensions.Logging;

namespace CrazyCards.Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@Request} at {@DateTimeUtc}", 
            typeof(TRequest).Name,
            DateTime.UtcNow);

        try
        {
            var result = await next();
            
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("Error handling request {@Request} at {@DateTimeUtc} with error {@Error}", 
                typeof(TRequest).Name,
                DateTime.UtcNow,
                e.Message);
        }
        finally
        {
            _logger.LogInformation("Finished request {@Request} at {@DateTimeUtc}", 
                typeof(TRequest).Name,
                DateTime.UtcNow);
        }
        
        return default!;
    }
}