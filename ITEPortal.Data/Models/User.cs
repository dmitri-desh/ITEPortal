namespace ITEPortal.Data.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? DeletedDate { get; set; }
        public int UserRoleId { get; set; }
     
        public virtual UserRole UserRole { get; set; }
        public ICollection<AuthCode> AuthCodes { get; set; } = new List<AuthCode>();
    }
}
