using Contracts.Enums;

namespace ITEPortal.Domain.Dto
{
    public class CreateStandDto
    {
        public decimal Area { get; set; }
        public BuildingType BuildingType { get; set; }
        public StandConfiguration StandConfiguration { get; set; }
        public decimal SecondFloorArea { get; set; }
        public int StandNumber { get; set; }
        public int ExhibitorId { get; set; }
    }
}
