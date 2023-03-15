using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.ExhibitionFeatures.Queries;
using ITEPortal.Domain.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.ExhibitionFeatures.Handlers.QueryHandlers
{
    public class GetExhibitionsSummaryByUserIdQueryHandler : IRequestHandler<GetExhibitionsSummaryByUserIdQuery, List<ExhibitionSummary>>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GetExhibitionsSummaryByUserIdQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ExhibitionSummary>> Handle(GetExhibitionsSummaryByUserIdQuery request, CancellationToken cancellationToken)
        {
            var exhibitions = await _context.Set<Exhibition>()
                .Where(x => x.Exhibitors.Any(x => x.UserId == request.UserId))
                .ToListAsync();

            return _mapper.Map<List<ExhibitionSummary>>(exhibitions);
        }
    }
}
