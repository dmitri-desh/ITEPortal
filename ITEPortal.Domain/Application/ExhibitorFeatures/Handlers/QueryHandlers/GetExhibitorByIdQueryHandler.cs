using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.ExhibitionFeatures.Queries;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitorFeatures.Handlers.QueryHandlers
{
    public class GetExhibitorByIdQueryHandler : IRequestHandler<GetExhibitorByIdQuery, Exhibitor>
    {
        private readonly ApplicationContext _context;

        public GetExhibitorByIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Exhibitor?> Handle(GetExhibitorByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id < 0) return null;

            var exhibitor = await _context.Set<Exhibitor>()
                .Include(x => x.User)
                .Include(x => x.Exhibitions)
                .FirstOrDefaultAsync(x => x.UserId == request.Id);

            return exhibitor != null ? exhibitor : null;
        }
    }
}
