using ITEPortal.Domain.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITEPortal.Domain
{
    public static class Helper
    {
        public static ResponseDto GetErrorDto(Exception e)
        {
            return new ResponseDto
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }

        public static JwtSecurityToken GetToken(string username)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
               audience: AuthOptions.AUDIENCE,
               claims: claims,
               expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
               signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return jwt;
        }

        private class AuthOptions
        {
            public const string ISSUER = "AuthServer"; 
            public const string AUDIENCE = "AuthClient"; 
            const string KEY = "secretkey!1234567890"; 
            public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
