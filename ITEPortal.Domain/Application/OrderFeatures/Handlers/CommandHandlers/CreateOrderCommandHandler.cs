using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.OrderFeatures.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.OrderFeatures.Handlers.CommandHandlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly ApplicationContext _context;

        public CreateOrderCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDto = request.Order;

            var exhibitor = await _context.Set<Exhibitor>()
                .Include(it => it.SellingOffice)
                .FirstOrDefaultAsync(it => it.UserId == request.CurrentUserId);

            if (exhibitor == null)
            {
                throw new Exception("Exhibitor doesn't exist");
            }

            var nowDate = DateTimeOffset.UtcNow;
            var order = new Order
            {
                ExhibitionId = orderDto.ExhibitionId,
                ExhibitorId = exhibitor.Id,
                StandId = orderDto.StandId,
                CreatedDate = nowDate,
                LastModifiedDate = nowDate,
                Status = request.OrderStatus,
            };

            var priceListItems = await _context.Set<PriceListItem>()
                .Where(it => it.PriceList.SellingOfficeId == exhibitor.SellingOffice.Id)
                .Select(it => new
                {
                    Id = it.Id,
                    Name = it.Product.Name
                })
                .ToListAsync();

            var orderItems = orderDto.Products.Select(it => new OrderItem
            {
                PriceListItemId = priceListItems.Find(pli => pli.Name == it.Id)?.Id ?? 0,
                Amount = it.Amount,
                PeopleAmount = it.PeopleAmount,
                DayAmount = it.DayAmount,
                Language = it.Language,
                Order = order
            }).ToList();

            _context.Set<Order>().Add(order);
            _context.Set<OrderItem>().AddRange(orderItems);
            await _context.SaveChangesAsync();

            return order.Id;
        }
    }
}
