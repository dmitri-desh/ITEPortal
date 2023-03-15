using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.OrderFeatures.Commands;
using ITEPortal.Domain.Dto;
using ITEPortal.Domain.Services.Implementation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;

namespace ITEPortal.Domain.Application.OrderFeatures.Handlers.CommandHandlers
{
    public class GenerateOrderPdfCommandHandler : IRequestHandler<GenerateOrderPdfCommand, byte[]>
    {
        private readonly ApplicationContext _context;

        public GenerateOrderPdfCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<byte[]> Handle(GenerateOrderPdfCommand request, CancellationToken cancellationToken)
        {
            var orderId = request.OrderId;
            var orderWithData = await _context.Set<Order>()
                .Include(it => it.Exhibition)
                .Include(it => it.Exhibitor).ThenInclude(e => e.User)
                .FirstOrDefaultAsync(it => it.Id == orderId);

            if (orderWithData == null)
            {
                throw new Exception("No order id!");
            }

            var pdfOrder = new OrderPdfModel
            {
                OrderId = orderId,
                ExhibitionName = orderWithData.Exhibition.Name,
                ExhibitorEmail = orderWithData.Exhibitor.User.Email,
                OrderDate = orderWithData.CreatedDate
            };

            var orderItems = await _context.Set<OrderItem>()
                .Where(it => it.OrderId == orderId)
                .Select(it => new OrderItemPdfModel
                {
                    Name = it.PriceListItem.Product.Description ?? String.Empty,
                    Price = it.PriceListItem.Price,
                    Currency = it.PriceListItem.PriceList.SellingOffice.Currency,
                    Quantity = it.Amount > 0 ? it.Amount : it.PeopleAmount * it.DayAmount,
                })
                .ToListAsync();

            orderItems.ForEach(it => { it.Total = it.Price * it.Quantity; });

            pdfOrder.OrderItems = orderItems;
            pdfOrder.OrderTotal = orderItems.Sum(it => it.Total);
            pdfOrder.Currency = orderItems.FirstOrDefault()?.Currency ?? "RUB";

            var pdfService = new OrderPdfService(pdfOrder);

            return pdfService.GeneratePdf();
        }
    }
}
