using ITEPortal.Domain.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEPortal.Domain.Application.PriceListFeatures.Queries
{
    public class GetPriceListItemsFilter
    {
        public int? ExhibitionId { get; set; }
        public string? Currency { get; set; }
        public int? SellingOfficeId { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
    }

    public class GetPriceListItemsQuery : IRequest<List<PriceListItemSummary>>
    {
        public GetPriceListItemsQuery(GetPriceListItemsFilter filter)
        {
            Filter = filter;
        }

        public GetPriceListItemsFilter Filter { get; }
    }
}
