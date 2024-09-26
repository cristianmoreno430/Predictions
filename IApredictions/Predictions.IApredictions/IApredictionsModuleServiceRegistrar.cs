using Microsoft.Extensions.DependencyInjection;
using Predictions.SharedKernel;

namespace Predictions.IApredictions;

public class IApredictionsModuleServiceRegistrar : IRegisterModuleServices
{
  public static IServiceCollection ConfigureServices(IServiceCollection services)
  {
    services.AddMediatR(
      c => c.RegisterServicesFromAssemblies(typeof(AssemblyInfo).Assembly));

    services.AddScoped<IWeatherForecastService, ServerWeatherForecastService>();

    return services;
  }
}
