namespace ITEPortal.Domain.Dto
{
    public class UserDto
    {
        /// <summary>
        /// The Id of User
        /// </summary>
        /// <example>1</example>
        public long Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public long UserRoleId { get; set; }
    }
}
