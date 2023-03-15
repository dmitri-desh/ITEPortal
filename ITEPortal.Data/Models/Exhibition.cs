using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Models
{
    public class Exhibition : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? LogoUrl { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string Place { get; set; } = string.Empty;
        public virtual List<Exhibitor> Exhibitors { get; set; } = new List<Exhibitor>();

        public virtual List<Stand> Stands { get; set; } = new List<Stand>();

        public virtual ICollection<PriceList> PriceLists { get; set; } = new List<PriceList>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class ExhibitionConfiguration : IEntityTypeConfiguration<Exhibition>
    {
        public void Configure(EntityTypeBuilder<Exhibition> builder)
        {
        }
    }
}
