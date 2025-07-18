namespace LinkedIn_Notes.HttpClients.Models.Requests;

public record EODClientRequest : ListClientRequestBase
{
    public required IEnumerable<string> Symbols { get; set; }
}

/// <summary>
/// This record represents the response from the third-party service.
/// </summary>
public record EODClientResponse : ListClientResponseBase<EODClientResponseData>
{
    public EODClientError? Error { get; set; }
}

public record EODClientResponseData
{
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
    public string Date { get; set; }
}

public record EODClientError {
    public required string Code { get; set; }
    public required string Message { get; set; }
}
