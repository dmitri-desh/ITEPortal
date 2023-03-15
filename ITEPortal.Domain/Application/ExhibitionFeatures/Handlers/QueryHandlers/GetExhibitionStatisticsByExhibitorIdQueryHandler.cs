using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.ExhibitionFeatures.Queries;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitionFeatures.Handlers.QueryHandlers
{
    public class GetExhibitionStatisticsByExhibitorIdQueryHandler : IRequestHandler<GetExhibitionStatisticsByExhibitorIdQuery, Dictionary<int, int>>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GetExhibitionStatisticsByExhibitorIdQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Dictionary<int, int>> Handle(GetExhibitionStatisticsByExhibitorIdQuery request, CancellationToken cancellationToken)
        {
            return _context.Set<Stand>()
             .Where(s => s.ExhibitorId == request.UserId)
             .GroupBy(s => s.ExhibitionId)
             .ToDictionary(s => s.Key, s => s.Count());
        }
    }
}
