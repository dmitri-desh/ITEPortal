using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.ProductFeatures.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.ProductFeatures.Handlers.CommandHandlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, bool>
    {
        private readonly ApplicationContext _context;

        public AddProductCommandHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productCategory = await _context.Set<Category>().FirstOrDefaultAsync(c => c.Name == request.CategoryName);
            if (productCategory == null) return false;

            var productToAdd = new Product
            {
                Name = request.Name,
                Category = productCategory,
                Description = request.Description,
            };

            _context.Set<Product>().Add(productToAdd);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
