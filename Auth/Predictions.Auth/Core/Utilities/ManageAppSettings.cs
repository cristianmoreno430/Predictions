using Microsoft.Extensions.Configuration;

namespace Predictions.Auth.Core.Utilities
{
    public class ManageAppSettings
    {
        private readonly IConfiguration _configuration;

        public ManageAppSettings()
        {
            // Configura el ConfigurationBuilder
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Define la base de la ruta para buscar archivos de configuración
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Añade el archivo appsettings.json

            _configuration = builder.Build(); // Construye la configuración
        }

        // Método para obtener un valor específico basado en una clave
        public string? GetValue(string key)
        {
            return _configuration[key]; // Obtiene el valor de la configuración usando la clave
        }
    }
}
