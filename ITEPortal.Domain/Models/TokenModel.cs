namespace ITEPortal.Domain.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime? ExpiresUTC { get; set; }
    }
}
