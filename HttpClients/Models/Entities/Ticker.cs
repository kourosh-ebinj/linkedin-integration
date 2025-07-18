namespace LinkedIn_Notes.HttpClients.Models.Entities;

public record Ticker
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public bool HasIntraday { get; set; }
    public bool HasEod { get; set; }
    public string? Country { get; set; }
}

