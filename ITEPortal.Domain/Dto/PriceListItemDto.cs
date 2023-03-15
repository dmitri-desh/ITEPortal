namespace ITEPortal.Domain.Dto
{
    public class PriceListItemDto
    {
        public int ExhibitionId { get; set; }
        public string Id { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}
