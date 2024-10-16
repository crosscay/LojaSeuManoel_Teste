using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LojaSeuManoel.Application.Services
{
    public class AuthService : IAuthService
    {
    
        public string GenerateJwtToken()
        {
            var key = Encoding.UTF8.GetBytes("f8D&3j$kB!z@7Q^nP$e*1Yw%8WqM#2bT");
            var symmetricKey = new SymmetricSecurityKey(key);
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}