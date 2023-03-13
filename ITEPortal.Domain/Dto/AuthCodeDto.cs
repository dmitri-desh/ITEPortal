namespace ITEPortal.Domain.Dto
{
    public class AuthCodeDto
    {
        public int Id { get; set; }
        public string CodeNumber { get; set; } = string.Empty;
        public DateTime ExpiredDate { get; set; }

        public int UserId { get; set; }
    }
}
