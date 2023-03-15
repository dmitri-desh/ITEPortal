using ITEPortal.Data.Models;
using MediatR;

namespace ITEPortal.Domain.Application.ProductFeatures.Queries
{
    public class GetProductsQueryFilter
    {
        public int? CategoryId { get; set; }
    }

    public class GetProductsQuery : IRequest<List<Product>>
    {
        public GetProductsQuery(GetProductsQueryFilter filter)
        {
            Filter = filter;
        }

        public GetProductsQueryFilter Filter { get; }
    }
}
