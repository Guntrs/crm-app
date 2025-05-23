using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace crm_app.Security
{
    public class CrmJwtService
    {
        private readonly string _key;
        private readonly string _issuer;

        public CrmJwtService(string key, string issuer)
        {
            _key = key;
            _issuer = issuer;
        }

        public string GenerateToken(long userId, string userName)
        {
            var claims = new[]
            {
                new Claim("userId", userId.ToString()),
                new Claim("userName", userName)
                // Puedes agregar más claims aquí si quieres (roles, email, etc.)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}