using Microsoft.EntityFrameworkCore.Storage;
using UnitOfWork.Data;
using UnitOfWork.Models;
using UnitOfWork.Repositories.Implementations;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.Unit_Of_Work;

public class UnitOfWorks : IUnitOfWorks
{
    private readonly AppDbContext _context; // bu DbContext ni saqlash uchun

    public UnitOfWorks(AppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        return new GenericRepository<TEntity>(_context);
    }

    public async Task BeginTransactionAsync()
    {
        if (_context.Database.CurrentTransaction == null)
            await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync(); // bu o'zgarishlarni saqlash uchun
            await _context.Database.CommitTransactionAsync(); // bu tranzaktsiyani yakunlash uchun
        }
        catch (Exception)
        {
            await _context.Database.RollbackTransactionAsync(); // bu tranzaktsiyani bekor qilish uchun
            throw; // xatoni tashlash
        }

    }
    public async Task RollbackTransactionAsync()
    {
        // joriy tranzaktsiyani olish
        var transaction = _context.Database.CurrentTransaction;

        if (transaction != null)
        {
            try
            {
                await transaction.RollbackAsync();
                // Transactionni bekor qilish
            }
            finally
            {
                await transaction.DisposeAsync(); // Transactionni yopish
                _context.ChangeTracker.Clear(); // Memorydagi o'zgarishlarni tozalash
            }
        }
    }
    public void Dispose()
    {
        _context.Dispose(); // bu dbu kontekstni tozalash uchun
    }
}
