using Microsoft.EntityFrameworkCore;
using UnitOfWork.Data;
using UnitOfWork.Repositories.Interfaces;
using UnitOfWork.Unit_Of_Work;

namespace UnitOfWork.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet; // bu DbSet orqali ma'lumotlar bazasi bilan ishlaymiz aynan bitta jadval bilan
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>(); // bu DbSetni olish uchun .// Set<T>() metodi orqali
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
    public  void Update(T entity)
    {
         _dbSet.Update(entity);
    }
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}
