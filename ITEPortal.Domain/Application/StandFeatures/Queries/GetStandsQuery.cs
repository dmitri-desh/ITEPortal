using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.StandFeatures.Queries
{
    public class GetStandsQuery : IRequest<List<StandDto>>
    {
        public int ExhibitorId { get; set; }
        public int ExhibitionId { get; set; }

        public GetStandsQuery(int exhibitorId, int exhibitionId)
        {
            ExhibitorId = exhibitorId;
            ExhibitionId = exhibitionId;
        }
    }
}
