using Contracts.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Models
{
    public class Order : BaseEntity
    {
        public OrderStatus Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }

        public int ExhibitionId { get; set; }
        public int ExhibitorId { get; set; }
        public int StandId { get; set; }

        public virtual Exhibition Exhibition { get; set; }
        public virtual Exhibitor Exhibitor { get; set; }
        public virtual Stand Stand { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
        }
    }
}
