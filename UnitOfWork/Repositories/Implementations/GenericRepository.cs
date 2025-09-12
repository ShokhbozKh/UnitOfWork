using Microsoft.EntityFrameworkCore;
using UnitOfWork.Data;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>(); // bu DbSetni olish uchun
    }


    public async Task<List<T>> GetAllAsync()
    {
        var result = await _dbSet.ToListAsync(); // bu hamma ma'lumotlarni olish uchun
        return result;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var result = await _dbSet.FindAsync(id); // bu id bo'yicha ma'lumot olish uchun
        return result;
    }
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity); // bu yangi ma'lumot qo'shish uchun
    }
    public async Task Update(T entity)
    {
         _dbSet.Update(entity);
    }
    public async Task Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
