using ITEPortal.Data.Models;
using ITEPortal.Domain.Application.ExhibitorFeatures.Commands;
using ITEPortal.Domain.Application.ExhibitorFeatures.Queries;
using ITEPortal.Domain.Application.PriceListFeatures.Queries;
using ITEPortal.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("pricelist-items")]
    [ApiController]
    public class PriceListItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PriceListItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPriceListItems([FromQuery] GetPriceListItemsFilter filter)
        {
            var priceListItems = await _mediator.Send(new GetPriceListItemsQuery(filter));
            return Ok(priceListItems);
        }
    }
}