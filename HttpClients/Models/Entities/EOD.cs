namespace LinkedIn_Notes.HttpClients.Models.Entities;

public record EOD
{
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
    public DateTime Date { get; set; }

}

