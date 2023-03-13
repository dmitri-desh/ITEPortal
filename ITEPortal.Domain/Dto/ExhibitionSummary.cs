namespace ITEPortal.Domain.Dto
{
    public class ExhibitionSummary
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Place { get; set; } = string.Empty;
    }
}
