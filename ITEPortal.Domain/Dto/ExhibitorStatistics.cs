namespace ITEPortal.Domain.Dto
{
    public class ExhibitorStatistics
    {
        public ExhibitionSummary ExhibitionSummary { get; set; } = new ExhibitionSummary();
        public int Stands { get; set; } = 0;
        public int Orders { get; set; } = 0;
    }
}
