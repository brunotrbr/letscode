using kanban_api.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace kanban_api.Utils.GenerateToken
{
    public class GenerateToken
    {
        private readonly TokenConfigurations _tokenConfiguration;
        public GenerateToken(TokenConfigurations tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
        }
        public string GenerateJWT(string username, string password)
        {
            var secret = _tokenConfiguration.Secret;
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var issuer = _tokenConfiguration.Issuer;
            var audience = _tokenConfiguration.Audience;
            var expirationInHours = _tokenConfiguration.ExpirationTimeInHours;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim("Username", username),
                }),
                Expires = DateTime.UtcNow.AddHours(expirationInHours),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
