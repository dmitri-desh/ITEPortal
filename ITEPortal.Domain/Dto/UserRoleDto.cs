namespace ITEPortal.Domain.Dto
{
    public class UserRoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool GlobalSettingsAllowed { get; set; } = false;
    }
}
