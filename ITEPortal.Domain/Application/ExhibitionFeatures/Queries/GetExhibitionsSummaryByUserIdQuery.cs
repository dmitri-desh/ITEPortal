using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.ExhibitionFeatures.Queries
{
    public class GetExhibitionsSummaryByUserIdQuery : IRequest<List<ExhibitionSummary>>
    {
        public int UserId { get; set; }

        public GetExhibitionsSummaryByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
