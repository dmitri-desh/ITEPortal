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

        public async Task<AuthCode> AddAuthCodeAsync(AuthCodeDto authCodeDto)
        {
            try
            {
                var generatedCode = await GenerateCodeAsync();
                authCodeDto.CodeNumber = generatedCode;
                //authCodeDto.ExpiredDate = DateTime.Now.AddHours(1);

                var authCode = await _authCodeRepository.InsertAsync(_mapper.Map<AuthCode>(authCodeDto));
                return authCode;
            }
            catch (Exception e)
            {
                return null; 
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

        public async Task<IEnumerable<AuthCode>> GetAllAsync()
        {
            var authCodes = await _authCodeRepository.GetAllAsync();
            
            return authCodes.ToList();
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

        public async Task<AuthCode> GetLastByUserIdAsync(long userId)
        {
            var authCodes = await GetAllAsync();
            var authCode = authCodes.OrderByDescending(code => code.Id).FirstOrDefault(x => x.User.Id == userId);

            return authCode;
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

        private async static Task<string> GenerateCodeAsync()
        {
            var code = await Nanoid.Nanoid.GenerateAsync("1234567890", 4);
            return code;
        }
    }
}
