
namespace LinkedIn_Notes.Models.Requests;

/// <summary>
/// This record represents the my application response.
/// </summary>
public record TickersRequest
{
    public int PageSize { get; set; }
}

public record TickersResponse : ListResponse<TickersResponseData>
{
}

public record TickersResponseData
{
    public required string Name { get; set; }
    public required string Symbol { get; set; }
    public bool Has_Intraday { get; set; }
    public bool Has_Eod { get; set; }
    public string? Country { get; set; }
}
