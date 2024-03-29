﻿using ITEPortal.Domain.Configuration;
using ITEPortal.Domain.Models;
using ITEPortal.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITEPortal.Domain.Services.Implementation
{
    public class TokenClaimsService : ITokenClaimsService
    {
        private readonly JwtSettings _options;
        private readonly IUserService _userService;
       
        public TokenClaimsService(IOptions<JwtSettings> options, IUserService userService)
        {
            _options = options.Value;
            _userService = userService;
         }

        public async Task<TokenModel> GetTokenAsync(string username)
        {
            var user = await _userService.GetByEmailAsync(username);

            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Name, username) 
            };

            var secretKey = Encoding.ASCII.GetBytes(_options.JwtTokenSecretKey);
            var securityKey = new SymmetricSecurityKey(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.Add(_options.TokenExpires),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var accessToken = tokenHandler.WriteToken(securityToken);

            var token = new TokenModel
            {
                AccessToken = accessToken,
                ExpiresUTC = tokenDescriptor.Expires,
            };

            return token;
        }

        public async Task<string> ValidateTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters 
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.JwtTokenSecretKey))
            };

            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            var claimName = claimsPrincipal.Claims.Where(x => x.Properties.Values.Contains("name")).Select(x => x.Value).FirstOrDefault();

            return claimName;
        }
    }
}
