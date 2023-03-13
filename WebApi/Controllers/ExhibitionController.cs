using ITEPortal.Data.Models;
using ITEPortal.Domain.Dto;
using ITEPortal.Domain.Application.ExhibitionFeatures.Commands;
using ITEPortal.Domain.Application.ExhibitionFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("exhibitions")]
    [Authorize]
    [ApiController]
    public class ExhibitionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExhibitionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ExhibitorStatistics>> GetAllExhibitions()
        {
            var userId = Convert.ToInt32(GetUserId());

            var exbitiorStatistics = await _mediator.Send(new GetExhibitionStatisticsByExhibitorIdQuery(userId));

            var exhibitionsSummary = await _mediator.Send(new GetExhibitionsSummaryByUserIdQuery(userId));

            var exhibitorStatistics = new List<ExhibitorStatistics>();

            foreach (var exhibitionSummary in exhibitionsSummary)
            {
                var ifContains = exbitiorStatistics.ContainsKey(exhibitionSummary.Id);
                if (ifContains)
                {
                    var exhibitorStatistic = new ExhibitorStatistics();
                    exhibitorStatistic.ExhibitionSummary = exhibitionSummary;
                    exhibitorStatistic.Stands = exbitiorStatistics.GetValueOrDefault(exhibitionSummary.Id);
                    exhibitorStatistics.Add(exhibitorStatistic);
                }
            }

            return exhibitorStatistics;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Exhibition> GetExhibitionById(int id)
        {
            return await _mediator.Send(new GetExhibitionByIdQuery(id));
        }

        [HttpPost]
        public async Task<int> CreateExhibition(ExhibitionDto exhibition)
        {
            return await _mediator.Send(new CreateExhibitionCommand(exhibition));
        }

        private string? GetUserId()
        {
            string? userId = (HttpContext.User.Identity as ClaimsIdentity)?.FindFirst("Id")?.Value;

            return userId;
        }
    }
}