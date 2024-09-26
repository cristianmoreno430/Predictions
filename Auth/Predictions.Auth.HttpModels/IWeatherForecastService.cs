namespace Predictions.Auth.HttpModels;

public interface IWeatherForecastService
{
  Task<IEnumerable<WeatherForecastResponse>> GetWeatherForecastAsync();
}
