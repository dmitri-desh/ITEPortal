using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Models
{
    public class PriceListItem : BaseEntity
    {
        public decimal Price { get; set; }

        public int ProductId { get; set; }
        public int PriceListId { get; set; }

        public Product Product { get; set; }
        public PriceList PriceList { get; set; }
    }

    public class PriceListItemConfiguration : IEntityTypeConfiguration<PriceListItem>
    {
        public void Configure(EntityTypeBuilder<PriceListItem> builder)
        {
        }
    }
}
