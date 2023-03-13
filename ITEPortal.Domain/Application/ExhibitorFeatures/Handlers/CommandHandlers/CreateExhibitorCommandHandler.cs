using Contracts.Enums;
using ITEPortal.Data.Models;
using ITEPortal.Data.Persistence;
using ITEPortal.Domain.Application.ExhibitorFeatures.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Domain.Application.ExhibitorFeatures.Handlers.CommandHandlers
{
    public class CreateExhibitorCommandHandler : IRequestHandler<CreateExhibitorCommand, int>
    {
        private readonly ApplicationContext _context;

        public CreateExhibitorCommandHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateExhibitorCommand request, CancellationToken cancellationToken)
        {
            var exhibitor = new Exhibitor();

            exhibitor.User = new User
            {
                Name = request.Exhibitor.Name,
                Email = request.Exhibitor.Email,
                Role = Role.Exhibitor
            };

            var exhibition = await _context.Set<Exhibition>().FirstOrDefaultAsync(x => x.Id == request.Exhibitor.ExhibitionId);

            if (exhibition == null) return 0;

            exhibitor.Exhibitions.Add(exhibition);

            await _context.Set<Exhibitor>().AddAsync(exhibitor);

            await _context.SaveChangesAsync();

            return exhibitor.UserId;
        }
    }
}
