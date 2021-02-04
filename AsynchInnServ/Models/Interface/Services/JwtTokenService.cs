using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface.Services
{
    public class JwtTokenService
    {
        public IConfiguration configuration;
        private SignInManager<AppUser> signinManager;

        public JwtTokenService(IConfiguration config, SignInManager<AppUser> manager)
        {
            configuration = config;
            signinManager = manager;
        }
        public static TokenValidationParameters GetValidationParams(IConfiguration configuration)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(configuration),
                ValidateIssuer = false,
                ValidateAudience = false
            };

        }
        //IConfiguration 
        public static SecurityKey GetSecurityKey(IConfiguration configuration)
        {
            var secret = configuration["JWT:Secret"];
            var secretBytes = Encoding.UTF8.GetBytes(secret);

            if (secret == null)
            {
                throw new InvalidOperationException("No JWT secret found");
            }

            return new SymmetricSecurityKey(secretBytes);
        }

        public async Task<string> GetToken(AppUser user, System.TimeSpan expiresIn)
        {
            var principal = await signinManager.CreateUserPrincipalAsync(user);
            if (principal == null) { return null; }

            var signingKey = GetSecurityKey(configuration);
            var token = new JwtSecurityToken(
              expires: DateTime.UtcNow + expiresIn,
              signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
              claims: principal.Claims
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
