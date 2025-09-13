using UnitOfWork.Models;

namespace UnitOfWork.Services.Users.Dtos;

public class UserUpdateDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
}
