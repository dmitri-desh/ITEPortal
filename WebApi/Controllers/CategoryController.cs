using ITEPortal.Data.Models;
using ITEPortal.Domain.Application.CategoryFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
   

    [Route("categories")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _mediator.Send(new GetAllCategoriesQuery());
        }
    }
}
