using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.ExhibitionFeatures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.ExhibitionFeatures.Handlers.QueryHandlers
{
    public class GetExhibitionByIdQuerryHandler : IRequestHandler<GetExhibitionByIdQuery, Exhibition>
    {
        private readonly ApplicationContext _context;

        public GetExhibitionByIdQuerryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Exhibition> Handle(GetExhibitionByIdQuery request, CancellationToken cancellationToken)
        {
            var exhibition = await _context.Set<Exhibition>().FirstOrDefaultAsync(x => x.Id == request.Id);

            if (exhibition != null) return exhibition;

            throw new Exception();
        }
    }
}
