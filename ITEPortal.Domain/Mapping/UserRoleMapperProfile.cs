using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Mapping
{
    public class UserRoleMapperProfile : Profile
    {
        public UserRoleMapperProfile() 
        {
            CreateMap<UserRole, UserRoleDto>();
            CreateMap<UserRoleDto, UserRole>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }
    }
}
