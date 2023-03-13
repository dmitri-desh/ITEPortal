using MediatR;

namespace ITEPortal.Domain.Application.ExhibitionFeatures.Queries
{
    public class GetExhibitionStatisticsByExhibitorIdQuery : IRequest<Dictionary<int, int>>
    {
        public int UserId { get; set; }

        public GetExhibitionStatisticsByExhibitorIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
