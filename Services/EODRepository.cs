using LinkedIn_Notes.HttpClients.Models.Entities;

namespace LinkedIn_Notes.Services;

public interface IEODRepository
{

    Task<EOD> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(IEnumerable<EOD> eods, CancellationToken cancellationToken = default);
}

public class EODRepository : IEODRepository
{
    public async Task<EOD> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return new (); // Placeholder for actual implementation
    }
    public async Task<int> CreateAsync(IEnumerable<EOD> eods, CancellationToken cancellationToken = default)
    {
        return 1; // Placeholder for actual implementation
    }
}
