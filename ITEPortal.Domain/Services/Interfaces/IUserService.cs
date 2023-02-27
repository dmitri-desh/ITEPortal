using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto> AddUserAsync(UserDto userDto);

        Task<ResponseDto> UpdateUserAsync(UserDto userDto);

        Task<ResponseDto> GetByIdAsync(long userId);

        Task<ResponseDto> GetAllAsync();

        Task<ResponseDto> DeleteUserAsync(UserDto userDto);
    }
}
