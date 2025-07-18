using LinkedIn_Notes.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkedIn_Notes.Controllers;

[ApiController]
[Route("[controller]")]
public class MarketStackController : ControllerBase
{
    private readonly IMarketStackService _marketStackService;
    private readonly ILogger<MarketStackController> _logger;

    public MarketStackController(IMarketStackService marketStackService, ILogger<MarketStackController> logger) 
    {
        _logger = logger;
        _marketStackService = marketStackService;
    }

    [HttpGet("EODs")]
    public async Task<IActionResult> GetEODs(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        
        var result = await _marketStackService.GetEODs(new Models.Requests.EODRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        }, cancellationToken);

        return Ok(result);
    }


    [HttpGet("Tickers")]
    public async Task<IActionResult> GetTickers(int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await _marketStackService.GetTickers(new Models.Requests.TickersRequest()
        {
            PageSize = pageSize,
        }, cancellationToken);

        return Ok(result);
    }
}
