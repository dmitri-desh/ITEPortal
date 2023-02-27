using ITEPortal.Data.Models;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Services.Interfaces
{
    public interface IAuthCodeService
    {
        Task<AuthCode> AddAuthCodeAsync(AuthCodeDto authCodeDto);

        Task<ResponseDto> UpdateAuthCodeAsync(AuthCodeDto authCodeDto);

        Task<ResponseDto> GetByIdAsync(long authCodeId);

        Task<IEnumerable<AuthCode>> GetAllAsync();

        Task<ResponseDto> DeleteAuthCodeAsync(AuthCodeDto authCodeDto);

        Task<AuthCode> GetLastByUserIdAsync(long userId);
    }
}
