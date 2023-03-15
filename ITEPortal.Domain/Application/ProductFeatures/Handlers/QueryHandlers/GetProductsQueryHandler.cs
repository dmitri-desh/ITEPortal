using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.ProductFeatures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.ProductFeatures.Handlers.QueryHandlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly ApplicationContext _context;

        public GetProductsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var productQuery = _context.Set<Product>().Include(p => p.Category).AsQueryable();

            if (filter.CategoryId != null)
            {
                productQuery = productQuery.Where(p => p.CategoryId == filter.CategoryId);
            }

            return await productQuery.ToListAsync();
        }
    }
}
