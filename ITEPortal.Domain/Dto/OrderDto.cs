namespace ITEPortal.Domain.Dto
{
    public class OrderDto
    {
        public int ExhibitionId { get; set; }
        public int StandId { get; set; }
        public int? OrderId { get; set; } = null;
        public IList<OrderItemDto> Products { get; set; } = new List<OrderItemDto>();
    }
}
