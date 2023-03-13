using ITEPortal.Data.Models;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitorFeatures.Queries
{
    public class GetExhibitorByIdQuery : IRequest<Exhibitor>
    {
        public int Id { get; }

        public GetExhibitorByIdQuery(int id)
        {
            Id = id;
        }
    }
}
