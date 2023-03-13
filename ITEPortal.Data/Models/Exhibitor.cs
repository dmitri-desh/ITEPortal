using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Models
{
    public class Exhibitor : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; } = new User();
        public virtual SellingOffice SellingOffice { get; set; } = new SellingOffice();

        public virtual List<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();
        public virtual List<Stand> Stands { get; set; } = new List<Stand>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class ExhibitorConfiguration : IEntityTypeConfiguration<Exhibitor>
    {
        public void Configure(EntityTypeBuilder<Exhibitor> builder)
        {
        }
    }
}
