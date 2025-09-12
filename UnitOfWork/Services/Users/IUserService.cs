using UnitOfWork.Models;
using UnitOfWork.Services.Users.Dtos;

namespace UnitOfWork.Services.Users
{
    public interface IUserService
    {
        Task<List<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto?> GetUserByIdAsync(int id);
        Task<int> AddUserAsync(UserCreateDto user);
        Task UpdateUserAsync(int id, UserUpdateDto updateUser);
        Task DeleteUserAsync(int id);
    }
}
