using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Mapping
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Role.Id, opt => opt.MapFrom(src => src.UserRoleId))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
