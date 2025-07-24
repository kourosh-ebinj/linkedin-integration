using LinkedIn_Notes.Abstractions;
using LinkedIn_Notes.HttpClients.DelegateHandlers;
using LinkedIn_Notes.HttpClients;
using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace LinkedIn_Notes.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddMarketStackClientService(this IServiceCollection services)
    {
        services.AddHttpClient<IMarketStackClientService, MarketStackClientService>(client =>
        {
            client.BaseAddress = new Uri("https://api.marketstack.com");
            client.Timeout = TimeSpan.FromSeconds(60); // default: 100 seconds
            client.DefaultRequestHeaders.Clear();
        })
            .AddHttpMessageHandler<LoggingDelegateHandler>()
            .AddPolicyHandler(GetResiliencePolicy());
        return services;
    }

    // Resilience Policy Definition
    static IAsyncPolicy<HttpResponseMessage> GetResiliencePolicy()
    {
        // Retry Policy
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError() // Handles 5xx and HttpRequestException
            .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests) // 429
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // Exponential backoff
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    Console.WriteLine($"Retry {retryAttempt} after {timespan.TotalSeconds}s due to: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}");
                });

        // Circuit Breaker Policy
        var circuitBreakerPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (outcome, breakDelay) =>
                {
                    Console.WriteLine($"Circuit broken due to: {outcome.Exception?.Message ?? outcome.Result?.StatusCode.ToString()}. Break for {breakDelay.TotalSeconds}s");
                },
                onReset: () => Console.WriteLine("Circuit closed again."),
                onHalfOpen: () => Console.WriteLine("Circuit is half-open. Trial request is allowed."));

        // Timeout Policy
        var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(30); // 10 seconds

        // Combine Policies (Wrap)
        return Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy);
    }
}
