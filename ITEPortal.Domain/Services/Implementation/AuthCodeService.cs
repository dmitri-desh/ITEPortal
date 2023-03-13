using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Data.Repositories.Interfaces;
using ITEPortal.Domain.Dto;
using ITEPortal.Domain.Services.Interfaces;

namespace ITEPortal.Domain.Services.Implementation
{
    public class AuthCodeService : IAuthCodeService
    {
        private readonly IAuthCodeRepository _authCodeRepository;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public AuthCodeService(IAuthCodeRepository authCodeRepository, IMapper mapper, ApplicationContext context)
        {
            _authCodeRepository = authCodeRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<AuthCode> AddAuthCodeAsync(int userId)
        {
            try
            {
                var generatedCode = await GenerateCodeAsync();
                var authCode = new AuthCode
                {
                    CodeNumber = generatedCode,
                    ExpiredDate = DateTime.UtcNow.AddHours(2)
                };

                var user = await _context.Set<User>().FindAsync(userId);
                user?.AuthCodes.Add(authCode);

                await _context.SaveChangesAsync();

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
