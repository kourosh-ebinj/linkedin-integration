
namespace LinkedIn_Notes.Models.Requests;

public record ListRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public record ListResponse<T>
{
    public required IEnumerable<T> Items { get; set; }
    public required ResponsePagination Pagination { get; set; }
}

public record ResponsePagination
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public int Offset { get; set; }
    public int Total { get; set; }
}
