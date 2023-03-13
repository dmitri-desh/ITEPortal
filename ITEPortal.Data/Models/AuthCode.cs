using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Models
{
    public class AuthCode : BaseEntity
    {
        public string CodeNumber { get; set; } = string.Empty;
        public DateTime ExpiredDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class AuthCodeConfiguration : IEntityTypeConfiguration<AuthCode>
    {
        public void Configure(EntityTypeBuilder<AuthCode> builder)
        {
        }
    }
}
