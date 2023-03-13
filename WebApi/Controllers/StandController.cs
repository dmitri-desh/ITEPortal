using ITEPortal.Data.Models;
using ITEPortal.Domain.Application.StandFeatures.Commands;
using ITEPortal.Domain.Application.StandFeatures.Queries;
using ITEPortal.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("/exhibitions/{exhibitionId}/stands")]
    [ApiController]
    public class StandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/exhibitions/{exhibitionId}/exhibitor-stands")]
        public async Task<List<StandDto>> GetStands(int exhibitionId)
        {
            var exhibitorId = Convert.ToInt32(GetUserId());

            return await _mediator.Send(new GetStandsQuery(exhibitorId, exhibitionId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStand(CreateStandDto stand, int exhibitionId)
        {
            string? userId = (HttpContext.User.Identity as ClaimsIdentity)?.FindFirst("Id")?.Value;

            if (userId == null) return Unauthorized(HttpStatusCode.Unauthorized);

            var result = await _mediator.Send(new CreateStandCommand(stand, exhibitionId));

            if (result != null) return Created("Stand has been created", result);

            return NotFound();
        }

        private string? GetUserId()
        {
            string? userId = (HttpContext.User.Identity as ClaimsIdentity)?.FindFirst("Id")?.Value;

            return userId;
        }
    }
}