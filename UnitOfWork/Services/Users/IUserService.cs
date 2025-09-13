using UnitOfWork.Models;
using UnitOfWork.Repositories.Interfaces;
using UnitOfWork.Services.Users.Dtos;

namespace UnitOfWork.Services.Users
{
    public interface IUserService : IGenericRepository<User>
    {
        Task<int> UserCountAsync();
        Task<List<UserReadDto>> GetAllAsync();
        Task<UserReadDto> GetByIdAsync(int id);
        Task<int> AddAsync(UserCreateDto userDto);
        Task UpdateAsync(int id, UserUpdateDto userDto);
        Task DeleteAsync(int id);

    }
}
