namespace ITEPortal.Domain.Models
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresUTC { get; set; }
    }
}
