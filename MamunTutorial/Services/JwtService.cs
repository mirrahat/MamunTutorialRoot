using MamunTutorial.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace MamunTutorial.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            Console.WriteLine("JwtService instantiated.");
        }

        public AuthenticationResponse CreateJwtToken(User user)
        {
            DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            try
            {
                JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims,
                    expires: expiration,
                    signingCredentials: signingCredentials
                );

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                string token = tokenHandler.WriteToken(tokenGenerator);

                return new AuthenticationResponse
                {
                    Token = token,
                    Email = user.Email,
                    Role = user.Role,
                    Expiration = expiration,
                    RefreshToken=GenerateRefreshToken(),
                    RefreshTokenExpirationDateTime= DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"]))
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating JWT token: {ex.Message}");
                throw;
            }
        }

        private string GenerateRefreshToken()
        {
            byte[] bytes = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }
    }
}
