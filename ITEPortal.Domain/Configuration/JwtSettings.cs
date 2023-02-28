namespace ITEPortal.Domain.Configuration
{
    public class JwtSettings
    {
        public string ConnectionString { get; set; }
        public TimeSpan TokenExpires { get; set; }
        public string JwtTokenSecretKey { get; set; }
    }
}
