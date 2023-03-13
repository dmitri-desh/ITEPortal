using Contracts.Enums;
using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.OrderFeatures.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderCommand(OrderDto order, OrderStatus orderStatus, int currentUserId)
        {
            Order = order;
            OrderStatus = orderStatus;
            CurrentUserId = currentUserId;
        }

        public OrderDto Order { get; }
        public OrderStatus OrderStatus { get; }
        public int CurrentUserId { get; }
    }
}
