namespace UnitOfWork.Models;

public class User: BaseModel
{
    public string FullName { get; set; }
    public ICollection<Order> Orders { get; set; }
    public Account Account { get; set; }

}
