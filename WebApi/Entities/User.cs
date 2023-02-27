namespace WebApi.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? DeletedDate { get; set; }

        public ICollection<AuthCode> AuthCodes { get; set; } = new List<AuthCode>();
    }
}
