using LinkedIn_Notes.HttpClients.DelegateHandlers;
using LinkedIn_Notes.Abstractions;
using LinkedIn_Notes.Services;
using LinkedIn_Notes.Extensions;

namespace LinkedIn_Notes;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<LoggingDelegateHandler>();
        builder.Services.AddScoped<IMarketStackService, MarketStackService>();
        builder.Services.AddScoped<IEODRepository, EODRepository>();
        builder.Services.AddScoped<ITickerRepository, TickerRepository>();

        builder.Services.AddMarketStackClientService();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();

    }
}
