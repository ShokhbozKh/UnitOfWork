namespace UnitOfWork.Models;

public class Order: BaseModel
{
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
