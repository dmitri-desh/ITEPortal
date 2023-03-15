using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.PriceListFeatures.Commands
{
    public class AddPriceListItemCommand : IRequest<bool>
    {
        public int ExhibitionId { get; set; }
        public string Id { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }

        public AddPriceListItemCommand(PriceListItemDto priceList)
        {
            ExhibitionId = priceList.ExhibitionId;
            Id = priceList.Id;
            Price = priceList.Price;
            Currency = priceList.Currency;
        }
    }
}
