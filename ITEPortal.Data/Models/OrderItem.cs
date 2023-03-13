using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Models
{
    public class OrderItem : BaseEntity
    {
        public int Amount { get; set; }
        public int DayAmount { get; set; }
        public int PeopleAmount { get; set; }
        public string Language { get; set; }

        public int OrderId { get; set; }
        public int PriceListItemId { get; set; }

        public virtual Order Order { get; set; }
        public virtual PriceListItem PriceListItem { get; set; }
    }

    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
        }
    }
}
