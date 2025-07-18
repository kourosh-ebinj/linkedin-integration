namespace LinkedIn_Notes.HttpClients.Models.Requests;

/// <summary>
/// This record represents the response from the third-party service.
/// </summary>
public record TickersClientRequest : ListClientRequestBase
{
    
    public required IEnumerable<string> Symbols { get; set; }
}

public record TickersClientResponse : ListClientResponseBase<TickersClientResponseData>
{
}

public record TickersClientResponseData
{
    public required string Name{ get; set; }
    public required string Symbol { get; set; }
    public bool Has_Intraday { get; set; }
    public bool Has_Eod { get; set; }
    public string? Country { get; set; }
}
