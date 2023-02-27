namespace ITEPortal.Data.Models
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool GlobalSettingsAllowed { get; set; } = false;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
