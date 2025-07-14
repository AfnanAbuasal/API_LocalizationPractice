namespace Ecommerce_API.Models
{
    public class BaseModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Status Status { get; set; } = Status.Active;
    }

    public enum Status
    {
        Inactive = 0,
        Active = 1
    }
}
