using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain.Mapping
{
    public class ExhibitionMapperProfile : Profile
    {
        public ExhibitionMapperProfile()
        {
            CreateMap<Exhibition, ExhibitionDto>();
            CreateMap<ExhibitionDto, Exhibition>();
            CreateMap<Exhibition, ExhibitionSummary>().ReverseMap();
        }
    }
}
