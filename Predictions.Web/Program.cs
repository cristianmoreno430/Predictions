using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Predictions.Web;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Call the method where you are registering services for each module:
// IApredictionsModuleServiceRegistrar.ConfigureServices(builder);

// Or use the discover method below to try and find the services for your modules
builder.Services.DiscoverAndRegisterModules();


builder.Services
  .AddAuthenticationJwtBearer(s => {
    // TODO: Add dotnet secrets
    s.SigningKey = builder.Configuration["Auth:JwtSecret"];
  })
  .AddAuthorization()
  .SwaggerDocument()
  .AddFastEndpoints();

var app = builder.Build();

app.UseHttpsRedirection();



// Use FastEndpoints
app.UseAuthentication()
  .UseAuthorization()  
  .UseSwaggerGen();




app.Run();

namespace Predictions.Web
{
  public partial class Program;
}