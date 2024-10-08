using FastEndpoints;

namespace Predictions.IApredictions;

internal record WeatherForecastResponse(DateOnly Date, int TemperatureC, string? Summary);
internal class WeatherForeCastEndpoint(IWeatherForecastService weatherForecastService) : EndpointWithoutRequest<IEnumerable<WeatherForecastResponse>>
{
  public override void Configure()
  {
    AllowAnonymous();
    Get("/IApredictions/weatherforecast");
  }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendOkAsync(await weatherForecastService.GetWeatherForecastAsync(), ct);
    }
}
