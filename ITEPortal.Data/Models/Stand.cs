using Contracts.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ITEPortal.Data.Models
{
    public class Stand : BaseEntity
    {
        public decimal Area { get; set; }

        public BuildingType BuildingType { get; set; }

        public StandConfiguration StandConfiguration { get; set; }

        public decimal SecondFloorArea { get; set; }

        public int StandNumber { get; set; }

        public int ExhibitorId { get; set; }

        public int ExhibitionId { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class StandModelConfiguration : IEntityTypeConfiguration<Stand>
    {
        public void Configure(EntityTypeBuilder<Stand> builder)
        {
        }
    }
}
