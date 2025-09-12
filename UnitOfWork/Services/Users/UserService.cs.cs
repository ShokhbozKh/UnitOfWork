using UnitOfWork.Models;
using UnitOfWork.Services.Users.Dtos;
using UnitOfWork.Unit_Of_Work;

namespace UnitOfWork.Services.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWorks _unitOfWork;
    public UserService(IUnitOfWorks unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<UserReadDto>> GetAllUsersAsync()
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();

        var userDtos = users.Select(u => new UserReadDto
        {
            Id = u.Id,
            Name = u.FullName,

        }).ToList();
        return userDtos;
    }

    public async Task<UserReadDto?> GetUserByIdAsync(int id)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);

        if (user == null) return null;

        var userDto = new UserReadDto
        {
            Id = user.Id,
            Name = user.FullName,
        };
        return userDto;
    }
    public async Task<int> AddUserAsync(UserCreateDto user)
    {
        var newUser = new User
        {
            FullName = user.FullName,
        };
        await _unitOfWork.Repository<User>().AddAsync(newUser);
        await _unitOfWork.CommitAsync(); // o'zgarishlarni saqlash
        return newUser.Id;

    }
    public async Task UpdateUserAsync(int id, UserUpdateDto updateUser)
    {
        var existingUser = await _unitOfWork.Repository<User>().GetByIdAsync(id);
        if (existingUser == null) return;

        existingUser.FullName = updateUser.FullName ?? existingUser.FullName;
        existingUser.FullName = updateUser.FullName ?? existingUser.FullName;

        await _unitOfWork.Repository<User>().Update(existingUser);
        await _unitOfWork.CommitAsync(); // o'zgarishlarni saqlash
    }
    public async Task DeleteUserAsync(int id)
    {
        var existingUser = await _unitOfWork.Repository<User>().GetByIdAsync(id);
        if (existingUser == null) return;
        await _unitOfWork.Repository<User>().Delete(existingUser);
        await _unitOfWork.CommitAsync(); // o'zgarishlarni saqlash
    }
}
