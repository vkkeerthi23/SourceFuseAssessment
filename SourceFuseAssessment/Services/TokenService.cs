using SourceFuseAssessment.Interfaces;
using System.Security.Claims;
using System.Text;
using SourceFuseAssessment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System;

namespace SourceFuseAssessment.Services
{
    public class TokenService : ITokenService
    {
        public IConfiguration Configuration { get; }
        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateToken()
        {
            var secretKey = Encoding.ASCII.GetBytes(Configuration["AppSettings:APIKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
