namespace ITEPortal.Data.Models
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
