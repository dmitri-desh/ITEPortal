using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.PriceListFeatures.Queries;
using ITEPortal.Domain.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.PriceListFeatures.Handlers.QueryHandlers
{
    public class GetPriceListItemsQueryHandler : IRequestHandler<GetPriceListItemsQuery, List<PriceListItemSummary>>
    {
        private readonly ApplicationContext _context;

        public GetPriceListItemsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<PriceListItemSummary>> Handle(GetPriceListItemsQuery request, CancellationToken cancellationToken)
        {
            var filter = request.Filter;
            var priceListItemsQuery = _context.Set<PriceListItem>().Include(it => it.PriceList).Include(it => it.Product).AsQueryable();

            if (filter?.Currency != null)
            {
                priceListItemsQuery = priceListItemsQuery.Where(it => it.PriceList.SellingOffice.Currency == filter.Currency);
            }

            if (filter?.ExhibitionId != null)
            {
                priceListItemsQuery = priceListItemsQuery.Where(it => it.PriceList.ExhibitionId == filter.ExhibitionId);
            }

            if (filter?.CategoryId != null)
            {
                priceListItemsQuery = priceListItemsQuery.Where(it => it.Product.CategoryId == filter.CategoryId);
            }

            var items = await priceListItemsQuery.Select(it => new
            {
                Id = it.Id,
                Name = it.Product.Name,
                Price = it.Price,
                Currency = it.PriceList.SellingOffice.Currency
            }).ToListAsync();

            return items.Select(it => new PriceListItemSummary
            {
                Id = it.Id,
                Name = it.Name,
                Price = it.Price,
                FormattedPrice = $"{it.Price} {it.Currency}"
            })
            .ToList();
        }
    }
}
