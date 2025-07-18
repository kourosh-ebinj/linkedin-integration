namespace LinkedIn_Notes.HttpClients.Models.Requests;

public record ListClientRequestBase
{
    // PageSize
    public int Limit { get; set; }
    // PageNumber 
    public int Count { get; set; }
    public int Offset { get; set; } = 0;
}

public record ListClientResponseBase<TData> {
    public IEnumerable<TData> Data { get; set; }
    public ClientPagination Pagination { get; set; }
}

public record ClientPagination
{
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int Count { get; set; }
    public int Total { get; set; }
}
