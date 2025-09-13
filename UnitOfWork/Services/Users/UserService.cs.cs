using Microsoft.EntityFrameworkCore;
using UnitOfWork.Data;
using UnitOfWork.Models;
using UnitOfWork.Repositories.Implementations;
using UnitOfWork.Services.Users.Dtos;
using UnitOfWork.Unit_Of_Work;

namespace UnitOfWork.Services.Users;

public class UserService : GenericRepository<User>, IUserService 
{
    protected readonly IUnitOfWorks _unitOfWorks;

    public UserService(AppDbContext context, IUnitOfWorks unitOfWorks) 
        : base(context)
    {
        _unitOfWorks = unitOfWorks;
    }
    public async Task<List<UserReadDto>> GetAllAsync()
    {
        // bu dbdan ma'lumotlarni olib UserReadDto ga map qilish uchun
        var result = await _unitOfWorks.Repository<User>().GetAllAsync();

        var userDtos = result.Select(user => new UserReadDto
        {
            Id = user.Id,
            FullName = user.Name,
            Email = user.Email,
            Orders = user.Orders

        }).ToList();
        return userDtos;
    }

    public async Task<UserReadDto> GetByIdAsync(int id)
    {
        var user = await _unitOfWorks.Repository<User>().GetByIdAsync(id);
        if (user == null) return null;
        var userDto = new UserReadDto
        {
            Id = user.Id,
            FullName = user.Name,
            Email = user.Email,
            Orders = user.Orders
        };
        return userDto;
    }
    public async Task<int> UserCountAsync()
    {
        var countUser = await _unitOfWorks.Repository<User>().GetAllAsync();
        return countUser.Count();
    }
    public async Task<int> AddAsync(UserCreateDto userDto)
    {
        await _unitOfWorks.BeginTransactionAsync();

        try
        {
            var user = new User
            {
                Name = userDto.FullName,
                Email = userDto.Email,
            };

            await _unitOfWorks.Repository<User>().AddAsync(user);
            // bu yerda boshqa bir nechta operatsiyalar bo'lishi mumkin
            // masalan, boshqa jadvallarga ma'lumot qo'shish yoki yangilash
            // agar hamma narsa muvaffaqiyatli bo'lsa, tranzaktsiyani yakunlaymiz
            // lekin bu yerda faqat bitta operatsiya bor
            await _unitOfWorks.CommitAsync();
            

            return user.Id;

        }
        catch (Exception)
        {
            await _unitOfWorks.RollbackTransactionAsync();
        }
        // agar xatolik yuz bersa, tranzaktsiyani bekor qilamiz va 0 qaytaramiz
        return 0;

    }
    public async Task UpdateAsync(int id, UserUpdateDto userDto)
    {
        await _unitOfWorks.BeginTransactionAsync();

        try
        {
            var user = await _unitOfWorks.Repository<User>().GetByIdAsync(id);
            if (user == null) return;


            user.Name = userDto.FullName ?? user.Name;
            user.Email = userDto.Email ?? user.Email;

            _unitOfWorks.Repository<User>().Update(user);
            await _unitOfWorks.CommitAsync();

        }
        catch (Exception)
        {
            await _unitOfWorks.RollbackTransactionAsync();
        }
        
    }
    public async Task DeleteAsync(int id)
    {
        await _unitOfWorks.BeginTransactionAsync();

        try
        {
            var user = await _unitOfWorks.Repository<User>().GetByIdAsync(id);
            if (user == null) return;
            _unitOfWorks.Repository<User>().Delete(user);
            await _unitOfWorks.CommitAsync();

        }
        catch (Exception)
        {
            await _unitOfWorks.RollbackTransactionAsync();
        } 
    }

}
