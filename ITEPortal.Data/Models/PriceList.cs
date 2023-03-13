using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Models
{
    public class PriceList : BaseEntity
    {
        public int ExhibitionId { get; set; }
        public int SellingOfficeId { get; set; }

        public virtual Exhibition Exhibition { get; set; }
        public virtual SellingOffice SellingOffice { get; set; }

        public virtual ICollection<PriceListItem> PriceListItems { get; set; } = new List<PriceListItem>();
    }

    public class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
    {
        public void Configure(EntityTypeBuilder<PriceList> builder)
        {
        }
    }
}
