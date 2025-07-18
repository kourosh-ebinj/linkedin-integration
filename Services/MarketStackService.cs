using LinkedIn_Notes.Abstractions;
using LinkedIn_Notes.Extensions;
using LinkedIn_Notes.Models.Requests;

namespace LinkedIn_Notes.Services;

public class MarketStackService : IMarketStackService
{
    private readonly IMarketStackClientService _marketStackClientService;
    private readonly IEODRepository _eODRepository;
    private readonly ITickerRepository _tickerRepository;
    private readonly ILogger<MarketStackService> _logger;

    static IEnumerable<string> myFixedSymbols = new List<string>() { "AAPL", "MSFT", "AMZN", "IAU" };

    public MarketStackService(IMarketStackClientService marketStackClientService,
        IEODRepository eODRepository,
        ITickerRepository tickerRepository,
        ILogger<MarketStackService> logger)
    {
        _marketStackClientService = marketStackClientService;
        _eODRepository = eODRepository;
        _tickerRepository = tickerRepository;
        _logger = logger;
    }

    public async Task<EODResponse> GetEODs(EODRequest request, CancellationToken cancellationToken = default)
    {
        var clientResult = await _marketStackClientService.GetEODs(
            new HttpClients.Models.Requests.EODClientRequest()
            {
                Symbols = myFixedSymbols,
                Limit = request.PageSize,
                Count = request.PageNumber,
            }, cancellationToken);

        if (!clientResult.IsSuccess)
        {
            //_logger.LogError("Failed to get EODs from MarketStackClientService.");
            
            // Handle failure
            throw new Exception($"{clientResult.Error?.Message} ({clientResult.Error?.Code})");
        }

        // Persist the client request/response data 
        await _eODRepository.CreateAsync(
            clientResult.Response!.Data.Select(e =>
                new HttpClients.Models.Entities.EOD()
                {
                    Close = e.Close,
                    Date = DateTime.Parse(e.Date),
                    High = e.High,
                    Low = e.Low,
                    Open = e.Open,
                    Volume = e.Volume,
                })
            , cancellationToken);

        return clientResult.Response!.GetEODResponse();
    }

    public async Task<TickersResponse> GetTickers(TickersRequest request, CancellationToken cancellationToken = default)
    {
        var clientResult = await _marketStackClientService.GetTickers(
            new HttpClients.Models.Requests.TickersClientRequest()
            {
                Symbols = myFixedSymbols,
                Limit = request.PageSize,
            }, cancellationToken);

        if (!clientResult.IsSuccess)
        {
            //_logger.LogError("Failed to get tickers from MarketStackClientService.");

            // Handle failure
            throw new Exception($"{clientResult.Error?.Message} ({clientResult.Error?.Code})");
        }

        // Persist the client request/response data for auditing purposes
        await _tickerRepository.CreateAsync(
            clientResult.Response!.Data.Select(e =>
                new HttpClients.Models.Entities.Ticker()
                {
                    Country = e.Country,
                    HasEod = e.Has_Eod,
                    HasIntraday = e.Has_Intraday,
                    Name = e.Name,
                    Symbol = e.Symbol,
                })
            , cancellationToken);

        return clientResult.Response!.GetTickersResponse();
    }
}
