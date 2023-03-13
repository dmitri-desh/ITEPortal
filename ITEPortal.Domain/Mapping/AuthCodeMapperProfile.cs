using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Mapping
{
    public class AuthCodeMapperProfile : Profile
    {
        public AuthCodeMapperProfile()
        {
            CreateMap<AuthCode, AuthCodeDto>();
            //CreateMap<AuthCodeDto, AuthCode>()
            //    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            //    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
