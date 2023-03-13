
namespace ITEPortal.Domain.Dto
{
    public class ExhibitionDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? LogoUrl { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Place { get; set; } = string.Empty;
    }
}
