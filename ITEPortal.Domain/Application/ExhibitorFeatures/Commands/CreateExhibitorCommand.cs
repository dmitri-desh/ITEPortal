using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitorFeatures.Commands
{
    public class CreateExhibitorCommand : IRequest<int>
    {
        public CreateExhibitorDto Exhibitor { get; set; }

        public CreateExhibitorCommand(CreateExhibitorDto exhibitor)
        {
            Exhibitor = exhibitor;
        }
    }
}
