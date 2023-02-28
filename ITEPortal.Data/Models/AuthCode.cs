namespace ITEPortal.Data.Models
{
    public class AuthCode : BaseEntity
    {
        public string CodeNumber { get; set; } = string.Empty;
        public DateTime ExpiredDate { get; set; }
 
        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}
