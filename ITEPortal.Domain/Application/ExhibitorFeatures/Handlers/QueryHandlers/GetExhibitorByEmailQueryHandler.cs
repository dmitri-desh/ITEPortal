using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitorFeatures.Handlers.QueryHandlers
{
    public class GetExhibitorByEmailQueryHandler : IRequestHandler<GetExhibitorByEmailQuery, Exhibitor>
    {
        private readonly ApplicationContext _context;

        public GetExhibitorByEmailQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Exhibitor?> Handle(GetExhibitorByEmailQuery request, CancellationToken cancellationToken)
        {
            var exhibitor = await _context.Set<Exhibitor>()
                .Include(x => x.User)
                .Include(x => x.Exhibitions)
                .FirstOrDefaultAsync(x => x.User.Email == request.Email);

            return exhibitor != null ? exhibitor : null;
        }
    }
}
