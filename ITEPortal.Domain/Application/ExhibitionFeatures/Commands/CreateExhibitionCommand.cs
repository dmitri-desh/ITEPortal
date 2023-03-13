using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitionFeatures.Commands
{
    public class CreateExhibitionCommand : IRequest<int>
    {
        public ExhibitionDto Exhibition { get; }

        public CreateExhibitionCommand(ExhibitionDto exhibition)
        {
            Exhibition = exhibition;
        }
    }
}
