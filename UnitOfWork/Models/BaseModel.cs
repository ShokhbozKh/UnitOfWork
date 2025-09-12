namespace UnitOfWork.Models;

public class BaseModel
{
    public int Id { get; set; }
    public DateTime CreatedAd { get; set; }= DateTime.UtcNow;
    public DateTime? UpdatedAd { get; set; }
    public bool IsDeleted { get; set; }= false;
}
