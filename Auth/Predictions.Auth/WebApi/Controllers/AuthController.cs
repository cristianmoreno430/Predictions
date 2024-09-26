using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Predictions.Auth.WebApi.Models;

namespace Predictions.Auth.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous] // Desactiva la validación de CSRF para este controlador
    public class AuthController : ControllerBase
    {
        
        private readonly IAntiforgery _antiforgery;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAntiforgery antiforgery,
            IConfiguration config, ILogger<AuthController> logger)
        {            
            _antiforgery = antiforgery;
            _config = config;
            _logger = logger;
        }

        [HttpPost("Login")]
        internal IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Datos de inicio de sesión no válidos");
            }

            // Simular la validación de credenciales           


            // Aquí deberías validar las credenciales del usuario
          //  var userId = "userId"; // Obtener del modelo de usuario autenticado
            var userName = model.username;

            var token = "";//_authService.GenerateToken(userId, userName);

            // Configura la cookie para almacenar el JWT
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Solo accesible por el servidor
                Secure = true, // Solo se enviará sobre HTTPS
                SameSite = SameSiteMode.None, // Protección contra CSRF
                Expires = DateTime.UtcNow.AddHours(1) // Expiración del token
            };

            Response.Cookies.Append("JWT-TOKEN", token, cookieOptions);

            // Configura la cookie para almacenar el token CSRF
            var cookieOptionsCSRF = new CookieOptions
            {
                HttpOnly = false, // Solo accesible por el servidor
                Secure = true, // Solo se enviará sobre HTTPS
                SameSite = SameSiteMode.None, // Protección contra CSRF
                Expires = DateTime.UtcNow.AddHours(1) // Expiración del token
            };

            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            Response.Cookies.Append("XSRF-TOKEN", "" /*tokens.RequestToken*/, cookieOptionsCSRF);


            // Devolver el token también en el cuerpo de la respuesta
            return Ok(new { Message = "Login successful." });

        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Eliminar las cookies de JWT y CSRF
            Response.Cookies.Delete("JWT-TOKEN");
            Response.Cookies.Delete("XSRF-TOKEN");

            return Ok(new { Message = "Logged out." });
        }
    }
}

