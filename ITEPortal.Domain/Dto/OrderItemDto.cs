namespace ITEPortal.Domain.Dto
{
    public class OrderItemDto
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public int DayAmount { get; set; }
        public int PeopleAmount { get; set; }
        public string Language { get; set; }
    }
}
