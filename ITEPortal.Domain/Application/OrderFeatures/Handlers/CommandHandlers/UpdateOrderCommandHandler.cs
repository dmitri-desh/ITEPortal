using Contracts.Enums;
using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.OrderFeatures.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.OrderFeatures.Handlers.CommandHandlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly ApplicationContext _context;

        public UpdateOrderCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDto = request.Order;
            var nowDate = DateTimeOffset.UtcNow;
            var order = await _context.Set<Order>().Include(it => it.OrderItems).FirstOrDefaultAsync(it => it.Id == orderDto.OrderId);

            if (order == null || order.Status != OrderStatus.Draft)
            {
                return false;
            }

            var exhibitor = await _context.Set<Exhibitor>()
                .Include(it => it.SellingOffice)
                .FirstOrDefaultAsync(it => it.UserId == request.CurrentUserId);
            if (exhibitor == null)
            {
                return false;
            }

            order.OrderItems.Clear();
            await _context.SaveChangesAsync();

            var priceListItems = await _context.Set<PriceListItem>()
                .Where(it => it.PriceList.SellingOfficeId == exhibitor.SellingOffice.Id)
                .Select(it => new
                {
                    Id = it.Id,
                    Name = it.Product.Name
                })
                .ToListAsync();

            var newOrderItems = orderDto.Products.Select(it => new OrderItem
            {
                PriceListItemId = priceListItems.Find(pli => pli.Name == it.Id)?.Id ?? 0,
                Amount = it.Amount,
                PeopleAmount = it.PeopleAmount,
                DayAmount = it.DayAmount,
                Language = it.Language,
                Order = order
            }).ToList();

            order.Status = request.OrderStatus;
            order.LastModifiedDate = DateTime.Now;

            _context.Set<OrderItem>().AddRange(newOrderItems);
            _context.Set<Order>().Update(order);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
