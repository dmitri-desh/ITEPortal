using Contracts.Enums;
using ITEPortal.Domain.Application.OrderFeatures.Commands;
using ITEPortal.Domain.Application.OrderFeatures.Queries;
using ITEPortal.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
   

    [Route("orders")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SaveOrder([FromBody] OrderDto order)
        {
            var orderStatus = OrderStatus.Submitted;
            if (order.OrderId == null)
            {
                var orderId = await CreateOrder(order, orderStatus);

                return Ok(orderId);
            }

            var isUpdateSuccessfull = await UpdateOrder(order, orderStatus);

            return Ok(isUpdateSuccessfull);

        }

        [HttpPost]
        [Route("draft")]
        public async Task<IActionResult> SaveOrderDraft([FromBody] OrderDto order)
        {
            var orderStatus = OrderStatus.Draft;
            if (order.OrderId == null)
            {
                var orderId = await CreateOrder(order, orderStatus);

                return Ok(orderId);
            }

            var isUpdateSuccessfull = await UpdateOrder(order, orderStatus);

            return Ok(isUpdateSuccessfull);
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var userId = GetUserId();

            var orderDto = await _mediator.Send(new GetOrderByIdQuery(orderId, userId));

            if (orderDto == null)
            {
                return NotFound("Order not found");
            }

            return Ok(orderDto);
        }

        [HttpGet]
        [Route("{orderId}/pdf")]
        public async Task<IActionResult> GetOrderPdf(int orderId)
        {
            var pdfBytes = await _mediator.Send(new GenerateOrderPdfCommand(orderId));

            return File(pdfBytes, "application/octet-stream", $"order-{orderId}.pdf");
        }

        private async Task<int> CreateOrder(OrderDto order, OrderStatus status)
        {
            return await _mediator.Send(new CreateOrderCommand(order, status, GetUserId()));
        }

        private async Task<bool> UpdateOrder(OrderDto order, OrderStatus status)
        {
            return await _mediator.Send(new UpdateOrderCommand(order, status, GetUserId()));
        }

        private int GetUserId()
        {
            string? userIdString = (HttpContext.User.Identity as ClaimsIdentity)?.FindFirst("Id")?.Value;

            if (!int.TryParse(userIdString, out int userId))
            {
                throw new Exception("Cannot extract user id");
            }

            return userId;
        }
    }
}