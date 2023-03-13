using ITEPortal.Data.Models;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitionFeatures.Queries
{
    public class GetExhibitionByIdQuery : IRequest<Exhibition>
    {
        public int Id { get; }

        public GetExhibitionByIdQuery(int id)
        {
            Id = id;
        }
    }
}
