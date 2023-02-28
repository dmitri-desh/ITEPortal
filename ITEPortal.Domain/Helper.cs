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
    }
}
