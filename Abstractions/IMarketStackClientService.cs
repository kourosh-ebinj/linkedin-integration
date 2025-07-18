using LinkedIn_Notes.HttpClients.Models.Requests;

namespace LinkedIn_Notes.Abstractions;

public interface IMarketStackClientService
{
    Task<Result<EODClientResponse>> GetEODs(EODClientRequest request, CancellationToken cancellationToken = default);
    Task<Result<TickersClientResponse>> GetTickers(TickersClientRequest request, CancellationToken cancellationToken = default);
}
