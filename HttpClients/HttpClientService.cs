
namespace LinkedIn_Notes.HttpClients;

public abstract class HttpClientService<TClientService> 
{
    protected readonly HttpClient _httpClient;
    protected readonly ILogger<TClientService> _logger;

    public HttpClientService(HttpClient httpClient, ILogger<TClientService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    protected Result Success() => Result.Success();

    protected Result<T> Success<T>([NotNull] T value) where T : notnull
        => Result<T>.Success(value);

    protected Result Failure(string errorMessage, ErrorType errorType = ErrorType.Failure, string? errorCode = null) =>
        Result.Failure(errorMessage, errorType, errorCode);

    protected Result<T> Failure<T>(string errorMessage, ErrorType errorType = ErrorType.Failure, string? errorCode = null) =>
        Result<T>.Failure(new Error(errorMessage, errorType, errorCode));
}
