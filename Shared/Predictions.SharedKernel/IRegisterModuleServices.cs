using Microsoft.Extensions.DependencyInjection;

namespace Predictions.SharedKernel;

public interface IRegisterModuleServices
{
  static abstract IServiceCollection ConfigureServices(IServiceCollection services);
}
