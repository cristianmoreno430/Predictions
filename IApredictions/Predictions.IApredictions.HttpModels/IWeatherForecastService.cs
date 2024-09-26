namespace Predictions.IApredictions.HttpModels;

public interface IWeatherForecastService
{
  Task<IEnumerable<WeatherForecastResponse>> GetWeatherForecastAsync();
}
