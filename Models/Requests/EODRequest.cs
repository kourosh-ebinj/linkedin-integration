

namespace LinkedIn_Notes.Models.Requests;

public record EODRequest: ListRequest
{
    
}

/// <summary>
/// This record represents the my application response.
/// </summary>
public record EODResponse : ListResponse<EODResponseData>
{
}

public record EODResponseData
{
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
    public required DateTime Date { get; set; }
}
