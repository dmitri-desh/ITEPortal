using MediatR;

namespace ITEPortal.Domain.Application.OrderFeatures.Commands
{
    public class GenerateOrderPdfCommand : IRequest<byte[]>
    {
        public GenerateOrderPdfCommand(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}
