using Contracts.Enums;
using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.OrderFeatures.Commands
{
    public class UpdateOrderCommand : IRequest<bool>
    {
        public UpdateOrderCommand(OrderDto order, OrderStatus orderStatus, int currentUserId)
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
