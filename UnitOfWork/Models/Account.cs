namespace UnitOfWork.Models;

public class Account: BaseModel
{
    public int UserId { get; set; }
    public decimal Balance { get; set; }
    public User User { get; set; } = null!;
}
