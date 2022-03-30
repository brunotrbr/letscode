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
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenConfiguration.Secret));
            var tokenHandler = new JwtSecurityTokenHandler();

            var claim = new Claim("sub", username);
            List<Claim> claims = new List<Claim>();
            claims.Add(claim);

            var jwtToken = new JwtSecurityToken(
                issuer: _tokenConfiguration.Issuer,
                audience: _tokenConfiguration.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_tokenConfiguration.ExpirationTimeInHours),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(jwtToken);
        }
    }
}
