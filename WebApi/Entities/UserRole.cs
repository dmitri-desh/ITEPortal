namespace WebApi.Entities
{
    public class UserRole
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool GlobalSettingsAllowed { get; set; } = false;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
