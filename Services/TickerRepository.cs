using LinkedIn_Notes.HttpClients.Models.Entities;

namespace LinkedIn_Notes.Services;

public interface ITickerRepository
{

    Task<Ticker> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(IEnumerable<Ticker> tickers, CancellationToken cancellationToken = default);
}

public class TickerRepository : ITickerRepository
{
    public async Task<Ticker> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return new(); // Placeholder for actual implementation
    }
    public async Task<int> CreateAsync(IEnumerable<Ticker> tickers, CancellationToken cancellationToken = default)
    {
        return 1; // Placeholder for actual implementation
    }
}
