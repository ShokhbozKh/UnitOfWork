using UnitOfWork.Models;

namespace UnitOfWork.Services.Users.Dtos
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }= new List<Order>();
    }
}
