using ITEPortal.Data.Models;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitorFeatures.Queries
{
    public class GetExhibitorByEmailQuery : IRequest<Exhibitor>
    {
        public string Email { get; }

        public GetExhibitorByEmailQuery(string email)
        {
            Email = email;
        }
    }
}
