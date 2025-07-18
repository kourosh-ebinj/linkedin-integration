using LinkedIn_Notes.Abstractions;
using LinkedIn_Notes.HttpClients.DelegateHandlers;
using LinkedIn_Notes.HttpClients;
using Microsoft.Extensions.Http.Resilience;
using Polly;

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
                //.AddHttpMessageHandler<MarketStackDelegateHandler>()
                .AddHttpMessageHandler<LoggingDelegateHandler>()
                .AddResilienceHandler("MyResilienceStrategy", resilienceBuilder => // Adds resilience policy named "MyResilienceStrategy"
                {
                    // Retry Strategy configuration
                    resilienceBuilder.AddRetry(new HttpRetryStrategyOptions // Configures retry behavior
                    {
                        MaxRetryAttempts = 3, // Maximum retries before throwing an exception (default: 3)
                        //Delay = TimeSpan.FromSeconds(5), // Delay between retries (default: varies by strategy)
                        BackoffType = DelayBackoffType.Exponential, // Exponential backoff for increasing delays (default)
                        //UseJitter = true, // Adds random jitter to delay for better distribution (default: false)
                        OnRetry = (options) =>
                        {
                            Console.WriteLine($"My Retry #{0}: {1}", options.AttemptNumber + 1, options.Outcome.Result?.ToString());
                            return default;
                        },
                        ShouldHandle = new PredicateBuilder<HttpResponseMessage>() // Defines exceptions to trigger retries
                        .Handle<HttpRequestException>() // Includes any HttpRequestException
                        .HandleResult(response => !response.IsSuccessStatusCode) // Includes non-successful responses
                    });
                   
                    // Timeout Strategy configuration
                    resilienceBuilder.AddTimeout(TimeSpan.FromSeconds(40)); // Sets a timeout limit for requests (throws TimeoutRejectedException)
                });

        return services;
    }
}
