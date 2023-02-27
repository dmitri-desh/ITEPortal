using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Services.Interfaces
{
    public interface IAuthCodeService
    {
        Task<ResponseDto> AddAuthCodeAsync(AuthCodeDto authCodeDto);

        Task<ResponseDto> UpdateAuthCodeAsync(AuthCodeDto authCodeDto);

        Task<ResponseDto> GetByIdAsync(long authCodeId);

        Task<ResponseDto> GetAllAsync();

        Task<ResponseDto> DeleteAuthCodeAsync(AuthCodeDto authCodeDto);
    }
}
