namespace ITEPortal.Domain.Dto
{
    public class StandDto
    {
        public int StandId { get; set; }
        public int ExhibitionId { get; set; }
        public int StandNumber { get; set; }
        public int? Pavilion { get; set; }
        public int? Hall { get; set; }
        public List<StandOrderDto> Orders { get; set; }
    }
}
