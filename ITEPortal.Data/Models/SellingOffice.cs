using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Models
{
    public class SellingOffice : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Currency { get; set; } = "RUB";

        public virtual ICollection<Exhibitor> Exhibitors { get; set; } = new List<Exhibitor>();
    }

    public class SellingOfficeConfiguration : IEntityTypeConfiguration<SellingOffice>
    {
        public void Configure(EntityTypeBuilder<SellingOffice> builder)
        {
        }
    }
}
