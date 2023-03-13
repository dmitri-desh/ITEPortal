using ITEPortal.Data.Models;
using MediatR;

namespace ITEPortal.Domain.Application.CategoryFeatures.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<Category>>
    {
    }
}
