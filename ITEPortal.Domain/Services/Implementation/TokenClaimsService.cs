using ITEPortal.Domain.Configuration;
using ITEPortal.Domain.Models;
using ITEPortal.Domain.Services.Interfaces;
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
        private readonly IUserRoleService _roleService;

        public TokenClaimsService(IOptions<JwtSettings> options, IUserService userService, IUserRoleService roleService)
        {
            _options = options.Value;
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<TokenModel> GetTokenAsync(string username)
        {
            var user = await _userService.GetByEmailAsync(username);
            var role = await _roleService.GetRoleByIdAsync(user.UserRoleId);

            var claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role.Name) 
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
    }
}
