using Contracts.Enums;
using ITEPortal.Domain.Dto;
using MediatR;

namespace ITEPortal.Domain.Application.StandFeatures.Commands
{
    public class CreateStandCommand : IRequest<int?>
    {
        public decimal Area { get; set; }
        public BuildingType BuildingType { get; set; }
        public StandConfiguration StandConfiguration { get; set; }
        public decimal SecondFloorArea { get; set; }
        public int StandNumber { get; set; }
        public int ExhibitorId { get; set; }
        public int ExhibitionId { get; set; }

        public CreateStandCommand(CreateStandDto stand, int exhibitionId)
        {
            Area = stand.Area;
            BuildingType = stand.BuildingType;
            StandConfiguration = stand.StandConfiguration;
            SecondFloorArea = stand.SecondFloorArea;
            StandNumber = stand.StandNumber;
            ExhibitorId = stand.ExhibitorId;
            ExhibitionId = exhibitionId;
        }
    }
}
