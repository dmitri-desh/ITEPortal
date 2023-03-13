using ITEPortal.Data.Models;
using ITEPortal.Domain.Application.ExhibitorFeatures.Commands;
using ITEPortal.Domain.Application.ExhibitorFeatures.Queries;
using ITEPortal.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("exhibitors")]
    [ApiController]
    public class ExhibitorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExhibitorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Exhibitor> GetExhibitors([FromQuery] string email)
        {
            return await _mediator.Send(new GetExhibitorByEmailQuery(email));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Exhibitor> GetExhibitionById(int id)
        {
            return await _mediator.Send(new GetExhibitorByIdQuery(id));
        }

        [HttpPost]
        public async Task<int> CreateExhibitor(CreateExhibitorDto exhibitor)
        {
            return await _mediator.Send(new CreateExhibitorCommand(exhibitor));
        }
    }
}