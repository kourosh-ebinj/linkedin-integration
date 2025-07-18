using System.Net.Sockets;

namespace LinkedIn_Notes.HttpClients.DelegateHandlers;

public class LoggingDelegateHandler : DelegatingHandler
{
    private readonly ILogger _logger;

    public LoggingDelegateHandler(ILogger<LoggingDelegateHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var reqLogData = new
            {
                RequestUri = $"{request.RequestUri} ({request.Method})",
                RequestHeader = request.Headers,
                RequestContent = request.Content is not null ? await request.Content.ReadAsStringAsync() : null,
            };
            _logger.LogInformation("HttpClient Request logged: {0}", reqLogData);

            var response = await base.SendAsync(request, cancellationToken);

            var resLogData = new
            {
                RequestUri = $"{request.RequestUri} ({request.Method})",
                ResponseHeader = response.Headers,
                ResponseContent = request.Content is not null ? await response.Content.ReadAsStringAsync() : "no content",
                ResponseStatusCode = response.StatusCode,
            };
            _logger.LogInformation("HttpClient Response logged: {0}", resLogData);

            return response;
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException)
        {
            // Any socket issues.
            // If Internet is not reachable, this exception occurs.
            throw;
        }
        catch (HttpRequestException ex)
        {
            var logData = new
            {
                Request = request,
            };
            _logger.LogError(ex, "HttpClient call encountered with an error: {0}", logData);

            throw;
        }
    }
}
