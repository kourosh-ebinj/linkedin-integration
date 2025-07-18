using LinkedIn_Notes.HttpClients.Models.Requests;
using LinkedIn_Notes.Models.Requests;

namespace LinkedIn_Notes.Extensions;

public static class MappingExtensions
{
    public static EODResponse GetEODResponse(this EODClientResponse model) =>
        new EODResponse()
        {
            Items = model.Data.Select(e => e.GetEODResponseData()),
            Pagination = new ResponsePagination()
            {
                PageNumber = model.Pagination.Count,
                PageSize = model.Pagination.Limit,
                Offset = model.Pagination.Offset,
                Total = model.Pagination.Total
            },
        };

    public static EODResponseData GetEODResponseData(this EODClientResponseData model) =>
        new EODResponseData()
        {
            Open = model.Open,
            High = model.High,
            Low = model.Low,
            Close = model.Close,
            Volume = model.Volume,
            Date = GetDateTimeOrDefault(model.Date)
        };

    public static TickersResponse GetTickersResponse(this TickersClientResponse model) =>
      new TickersResponse()
      {
          Items = model.Data.Select(e => e.GetTickersResponseData()),
          Pagination = new ResponsePagination()
          {
              PageNumber = model.Pagination.Count,
              PageSize = model.Pagination.Limit,
              Offset = model.Pagination.Offset,
              Total = model.Pagination.Total
          },
      };

    public static TickersResponseData GetTickersResponseData(this TickersClientResponseData model) =>
        new TickersResponseData()
        {
            Name = model.Name,
            Symbol = model.Symbol,
            Country = model.Country,
            Has_Intraday = model.Has_Intraday,
            Has_Eod = model.Has_Eod,
        };

    private static DateTime GetDateTimeOrDefault(string date)
    {
        if (!DateTime.TryParse(date, out var result)) return default;

        return result;
    }
}
