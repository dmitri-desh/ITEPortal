using ITEPortal.Data.Models;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Services.Interfaces
{
    public interface IAuthCodeService
    {
        Task<AuthCode> AddAuthCodeAsync(int userId);

        Task<ResponseDto> UpdateAuthCodeAsync(AuthCodeDto authCodeDto);

        Task<ResponseDto> GetByIdAsync(long authCodeId);

        Task<AuthCode> GetLastByUserIdAsync(long userId);
    }
}
