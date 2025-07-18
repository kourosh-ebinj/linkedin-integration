using LinkedIn_Notes.Abstractions;
using LinkedIn_Notes.HttpClients.Models.Requests;
using System.Net.Http;
using System.Text;

namespace LinkedIn_Notes.HttpClients;

public class MarketStackClientService : HttpClientService<MarketStackClientService>, IMarketStackClientService
{
    // Hard-coded for clarity; keep this setting in appsettings or environment variables in production code.
    const string accessKey = "f798b5a2924a2a6d9d7e5b5f6e3e5f8b";

    public MarketStackClientService(HttpClient httpClient, ILogger<MarketStackClientService> logger) : base(httpClient, logger)
    {
        
    }

    public async Task<Result<EODClientResponse>> GetEODs(EODClientRequest request, CancellationToken cancellationToken = default)
    {
        var queryBuilder = new StringBuilder();
        if (request.Symbols.Count() < 1)
            throw new ArgumentOutOfRangeException(nameof(request.Symbols), "Symbols must contain at least one symbol.");

        queryBuilder.Append($"?access_key={accessKey}");
        queryBuilder.Append($"&symbols={string.Join(",", request.Symbols)}");
        queryBuilder.Append($"&limit={request.Limit}");
        queryBuilder.Append($"&offset={request.Offset}");

        var uri = new Uri(_httpClient.BaseAddress!, $"/v1/eod{queryBuilder}");

        try
        {
            var response = await _httpClient.GetAsync(uri, cancellationToken: cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                return Failure<EODClientResponse>($"Error fetching EOD data: {errorMessage}");
            }

            var result = await response.Content.ReadFromJsonAsync<EODClientResponse>(cancellationToken);
            if (result is null)
                return Failure<EODClientResponse>("Response json is not valid.");

            return Success(result);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "GetEODs failed.");

            return Failure<EODClientResponse>(ex.Message);
        }
    }

    public async Task<Result<TickersClientResponse>> GetTickers(TickersClientRequest request, CancellationToken cancellationToken = default)
    {
        var queryBuilder = new StringBuilder();
        if (request.Symbols.Count() < 1)
            throw new ArgumentOutOfRangeException(nameof(request.Symbols), "Symbols must contain at least one symbol.");

        queryBuilder.Append($"?access_key={accessKey}");
        queryBuilder.Append($"&symbols={string.Join(",", request.Symbols)}");
        queryBuilder.Append($"&limit={request.Limit}");
        queryBuilder.Append($"&count={request.Count}");
        queryBuilder.Append($"&offset={request.Offset}");

        var uri = new Uri(_httpClient.BaseAddress!, $"/v1/tickers{queryBuilder}");

        try
        {
            var response = await _httpClient.GetAsync(uri, cancellationToken: cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync(cancellationToken);
                return Failure<TickersClientResponse>($"Error fetching tickers data: {errorMessage}");
            }

            var result = await response.Content.ReadFromJsonAsync<TickersClientResponse>(cancellationToken);
            if (result is null)
                return Failure<TickersClientResponse>("Response json is not valid.");

            return Success(result);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "GetTickers failed.");

            return Failure<TickersClientResponse>(ex.Message);
        }
    }
}
