using CsvHelper;
using ITEPortal.Data.Models;
using ITEPortal.Domain.Application.ProductFeatures.Commands;
using ITEPortal.Domain.Application.ProductFeatures.Queries;
using ITEPortal.Domain.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Product>> GetProductsAsync(int? categoryId = null)
        {
            return await _mediator.Send(new GetProductsQuery(new GetProductsQueryFilter { CategoryId = categoryId }));
        }

        [HttpPost]
        [Route("batch")]
        public async Task<IActionResult> AddProducts(IFormFile importProductsFile)
        {
            using (var reader = new StreamReader(importProductsFile.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var products = csv.GetRecords<ProductDto>();
                var errorProducts = new List<ProductDto>();

                foreach (var product in products)
                {
                    var wasSuccessfullyAdded = await _mediator.Send(new AddProductCommand(product));
                    if (!wasSuccessfullyAdded)
                    {
                        errorProducts.Add(product);
                    }
                }

                return Ok(new { errors = errorProducts.Select(it => it.Id) });
            }
        }
    }
}