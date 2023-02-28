using ITEPortal.Domain.Models;

namespace ITEPortal.Domain.Services.Interfaces
{
    public interface IJwtService
    {
        TokenResultModel IsExpiredToken(string accessToken);
    }
}
