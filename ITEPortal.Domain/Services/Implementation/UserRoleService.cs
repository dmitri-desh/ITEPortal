using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Data.Repositories.Interfaces;
using ITEPortal.Domain.Dto;
using ITEPortal.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEPortal.Domain.Services.Implementation
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddUserRoleAsync(UserRoleDto userRoleDto)
        {
            try
            {

                var userRole = await _userRoleRepository.InsertAsync(_mapper.Map<UserRole>(userRoleDto));
                return new ResponseDto()
                {
                    Success = true,
                    Data = _mapper.Map<UserRoleDto>(userRole)
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<ResponseDto> DeleteUserRoleAsync(UserRoleDto userRoleDto)
        {
            try
            {
                await _userRoleRepository.DeleteAsync(_mapper.Map<UserRole>(userRoleDto));

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
                var userRoles = await _userRoleRepository.GetAllAsync();
                var userRolesDto = userRoles.Select(userRole => _mapper.Map<UserRoleDto>(userRole)).ToList();

                return new ResponseDto()
                {
                    Success = true,
                    Data = userRolesDto
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<ResponseDto> GetByIdAsync(long userRoleId)
        {
            try
            {
                var userRole = await _userRoleRepository.GetByIdAsync(userRoleId);
                var userRoleDto = _mapper.Map<UserRoleDto>(userRole);

                return new ResponseDto()
                {
                    Success = true,
                    Data = userRoleDto
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<UserRole> GetRoleByIdAsync(long userRoleId)
        {
            try
            {
                return await _userRoleRepository.GetByIdAsync(userRoleId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ResponseDto> UpdateUserRoleAsync(UserRoleDto userRoleDto)
        {
            try
            {
                await _userRoleRepository.UpdateAsync(_mapper.Map<UserRole>(userRoleDto));

                return new ResponseDto()
                {
                    Success = true,
                    Data = new UserRoleDto() { Id = userRoleDto.Id }
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }
    }
}
