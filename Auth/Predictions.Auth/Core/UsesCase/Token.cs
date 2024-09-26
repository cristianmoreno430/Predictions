using Microsoft.IdentityModel.Tokens;
using Predictions.Auth.Core.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Predictions.Auth.Core.UsesCase
{
    public class Token
    {
        public string GenerateToken(string userId, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Inicializar SigningKey usando la clave secreta            
            var manageAppSettings = new ManageAppSettings();

            // Obtén el valor del campo JwtSettings:SecretKey
            string? secretKey = manageAppSettings.GetValue("JwtSettings:SecretKey");
            SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.ASCII.GetBytes(secretKey!)
            );

            // Define los claims del token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            // Define el descriptor del token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = manageAppSettings.GetValue("JwtSettings:Issuer"),
                Audience = manageAppSettings.GetValue("JwtSettings:Audience"),
                Expires = DateTime.UtcNow.AddHours(1),// Expiración del token en 1 hora
                SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

