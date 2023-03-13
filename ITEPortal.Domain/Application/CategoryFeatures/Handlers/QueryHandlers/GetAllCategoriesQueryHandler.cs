using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.CategoryFeatures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.CategoryFeatures.Handlers.QueryHandlers
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<Category>>
    {
        private readonly ApplicationContext _context;

        public GetAllCategoriesQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Category>().ToListAsync();
        }
    }
}
