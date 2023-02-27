using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Data.Repositories.Implementation;
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
    public class AuthCodeService : IAuthCodeService
    {
        private readonly IAuthCodeRepository _authCodeRepository;
        private readonly IMapper _mapper;

        public AuthCodeService(IAuthCodeRepository authCodeRepository, IMapper mapper)
        {
            _authCodeRepository = authCodeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto> AddAuthCodeAsync(AuthCodeDto authCodeDto)
        {
            try
            {

                var authCode = await _authCodeRepository.InsertAsync(_mapper.Map<AuthCode>(authCodeDto));
                return new ResponseDto()
                {
                    Success = true,
                    Data = _mapper.Map<UserDto>(authCode)
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<ResponseDto> DeleteAuthCodeAsync(AuthCodeDto authCodeDto)
        {
            try
            {
                await _authCodeRepository.DeleteAsync(_mapper.Map<AuthCode>(authCodeDto));

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
                var authCodes = await _authCodeRepository.GetAllAsync();
                var authCodesDto = authCodes.Select(authCode => _mapper.Map<AuthCodeDto>(authCode)).ToList();

                return new ResponseDto()
                {
                    Success = true,
                    Data = authCodesDto
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<ResponseDto> GetByIdAsync(long authCodeId)
        {
            try
            {
                var authCode = await _authCodeRepository.GetByIdAsync(authCodeId);
                var authCodeDto = _mapper.Map<AuthCodeDto>(authCode);

                return new ResponseDto()
                {
                    Success = true,
                    Data = authCodeDto
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }

        public async Task<ResponseDto> UpdateAuthCodeAsync(AuthCodeDto authCodeDto)
        {
            try
            {
                await _authCodeRepository.UpdateAsync(_mapper.Map<AuthCode>(authCodeDto));

                return new ResponseDto()
                {
                    Success = true,
                    Data = new UserDto() { Id = authCodeDto.Id }
                };
            }
            catch (Exception e)
            {
                return Helper.GetErrorDto(e);
            }
        }
    }
}
