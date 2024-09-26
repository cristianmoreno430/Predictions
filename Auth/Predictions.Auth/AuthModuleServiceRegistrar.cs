using Microsoft.Extensions.DependencyInjection;
using Predictions.SharedKernel;

namespace Predictions.Auth;

public class AuthModuleServiceRegistrar : IRegisterModuleServices
{
  public static IServiceCollection ConfigureServices(IServiceCollection services)
  {
    services.AddMediatR(
      c => c.RegisterServicesFromAssemblies(typeof(AssemblyInfo).Assembly));

    

    return services;
  }
}
