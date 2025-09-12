namespace UnitOfWork.Models;

public class Order: BaseModel
{
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }

    public User User { get; set; }
}
