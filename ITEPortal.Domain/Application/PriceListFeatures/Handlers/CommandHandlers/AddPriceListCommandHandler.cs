using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.PriceListFeatures.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.PriceListFeatures.Handlers.CommandHandlers
{
    public class AddPriceListCommandHandler : IRequestHandler<AddPriceListItemCommand, bool>
    {
        private readonly ApplicationContext _context;

        public AddPriceListCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddPriceListItemCommand request, CancellationToken cancellationToken)
        {
            var sellingOffice = await _context.Set<SellingOffice>().FirstOrDefaultAsync(o => o.Currency == request.Currency);
            if (sellingOffice == null) return false;

            var product = await _context.Set<Product>().FirstOrDefaultAsync(x => x.Name == request.Id);
            if (product == null) return false;

            var priceList = await _context.Set<PriceList>()
                .FirstOrDefaultAsync(x => x.SellingOfficeId == sellingOffice.Id && x.ExhibitionId == request.ExhibitionId);

            if (priceList == null)
            {
                priceList = new PriceList { ExhibitionId = request.ExhibitionId, SellingOfficeId = sellingOffice.Id };

                await _context.Set<PriceList>().AddAsync(priceList);

                await _context.SaveChangesAsync();
            }

            var priceListItem = new PriceListItem { Product = product, Price = request.Price, PriceListId = priceList.Id };

            priceList.PriceListItems.Add(priceListItem);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
