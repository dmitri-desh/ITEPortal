namespace ITEPortal.Domain.Dto
{
    public class AuthCodeDto
    {
        public long Id { get; set; }
        public string CodeNumber { get; set; } = string.Empty;
        public DateTime ExpiredDate { get; set; }
    }
}
