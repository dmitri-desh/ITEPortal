using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Mapping
{
    public class StandMapperProfile : Profile
    {
        public StandMapperProfile()
        {
            CreateMap<Stand, StandDto>()
                .ForMember(x => x.StandId, opt => opt.MapFrom(x => x.Id))
                .ReverseMap();

            CreateMap<Order, StandOrderDto>()
                .ForMember(x => x.OrderId, opt => opt.MapFrom(x => x.Id));
        }
    }
}
