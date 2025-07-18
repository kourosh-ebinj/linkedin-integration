
using LinkedIn_Notes.Models.Requests;

namespace LinkedIn_Notes.Abstractions;

public interface IMarketStackService
{
    Task<EODResponse> GetEODs(EODRequest request, CancellationToken cancellationToken = default);
    Task<TickersResponse> GetTickers(TickersRequest request, CancellationToken cancellationToken = default);

}
