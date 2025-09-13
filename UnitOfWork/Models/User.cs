namespace UnitOfWork.Models;

public class User: BaseModel
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<Order> Orders { get; set; } = new();

}
