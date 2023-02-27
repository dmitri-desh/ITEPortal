namespace WebApi.Entities
{
    public class AuthCode
    {
        public long Id { get; set; }
        public string CodeNumber { get; set; } = string.Empty;
        public DateTime ExpiredDate { get; set; }
    }
}
