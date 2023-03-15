using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.ProductFeatures.Commands
{
    public class AddProductCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public AddProductCommand(ProductDto product)
        {
            Name = product.Id;
            Description = product.Description;
            CategoryName = product.CategoryId;
        }
    }
}
