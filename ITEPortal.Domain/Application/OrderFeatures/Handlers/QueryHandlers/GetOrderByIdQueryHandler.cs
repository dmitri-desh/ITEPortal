using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.OrderFeatures.Queries;
using ITEPortal.Domain.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.OrderFeatures.Handlers.QueryHandlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly ApplicationContext _context;

        public GetOrderByIdQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var orderId = request.OrderId;
            var order = await _context.Set<Order>().FindAsync(orderId);

            if (order == null) return null;

            var orderDto = new OrderDto
            {
                OrderId = orderId,
                ExhibitionId = order.ExhibitionId,
                StandId = order.StandId,
            };

            var orderItemDtos = await _context.Set<OrderItem>()
                .Where(it => it.OrderId == orderId)
                .Select(it => new OrderItemDto
                {
                    Id = it.PriceListItem.Product.Name,
                    Amount = it.Amount,
                    DayAmount = it.DayAmount,
                    PeopleAmount = it.PeopleAmount,
                    Language = it.Language,
                })
                .ToListAsync();

            orderDto.Products = orderItemDtos;

            return orderDto;
        }
    }
}
