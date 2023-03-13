using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.OrderFeatures.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public GetOrderByIdQuery(int orderId, int userId)
        {
            OrderId = orderId;
            UserId = userId;
        }

        public int OrderId { get; }
        public int UserId { get; }
    }
}
