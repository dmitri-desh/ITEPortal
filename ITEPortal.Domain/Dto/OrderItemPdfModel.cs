namespace ITEPortal.Domain.Dto
{
    public class OrderItemPdfModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderPdfModel
    {
        public int OrderId { get; set; }
        public string ExhibitorEmail { get; set; }
        public string ExhibitionName { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public List<OrderItemPdfModel> OrderItems { get; set; } = new List<OrderItemPdfModel>();
        public decimal OrderTotal { get; set; }
        public string Currency { get; set; }
    }
}
