using AutoMapper;
using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.ExhibitionFeatures.Commands;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitionFeatures.Handlers.CommandHandlers
{
    public class CreateExhibitionCommandHandler : IRequestHandler<CreateExhibitionCommand, int>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CreateExhibitionCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateExhibitionCommand request, CancellationToken cancellationToken)
        {
            var exhibition = _mapper.Map<Exhibition>(request.Exhibition);

            await _context.Set<Exhibition>().AddAsync(exhibition);

            await _context.SaveChangesAsync();

            return exhibition.Id;
        }
    }
}
