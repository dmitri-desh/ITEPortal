using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<ResponseDto> AddUserRoleAsync(UserRoleDto userRoleDto);

        Task<ResponseDto> UpdateUserRoleAsync(UserRoleDto userRoleDto);

        Task<ResponseDto> GetByIdAsync(long userRoleId);

        Task<ResponseDto> GetAllAsync();

        Task<ResponseDto> DeleteUserRoleAsync(UserRoleDto userRoleDto);
    }
}
