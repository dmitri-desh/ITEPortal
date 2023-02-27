using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Data.Repositories.Interfaces;
using ITEPortal.Domain.Dto;
using ITEPortal.Domain.Services.Interfaces;

namespace ITEPortal.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddUserAsync(UserDto userDto)
        {
            try
            {

                var user = await _userRepository.InsertAsync(_mapper.Map<User>(userDto));
                return new ResponseDto()
                {
                    Success = true,
                    Data = _mapper.Map<UserDto>(user)
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<ResponseDto> DeleteUserAsync(UserDto userDto)
        {
            try
            {
                await _userRepository.DeleteAsync(_mapper.Map<User>(userDto));

                return new ResponseDto()
                {
                    Success = true
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<ResponseDto> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                var usersDto = users.Select(user => _mapper.Map<UserDto>(user)).ToList();

                return new ResponseDto()
                {
                    Success = true,
                    Data = usersDto
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<ResponseDto> GetByIdAsync(long userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                var userDto = _mapper.Map<UserDto>(user);

                return new ResponseDto()
                {
                    Success = true,
                    Data = userDto
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<ResponseDto> UpdateUserAsync(UserDto userDto)
        {
            try
            {
                await _userRepository.UpdateAsync(_mapper.Map<User>(userDto));

                return new ResponseDto()
                {
                    Success = true,
                    Data = new UserDto() { Id = userDto.Id }
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }
    }
}
