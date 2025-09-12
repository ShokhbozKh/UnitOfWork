using Microsoft.EntityFrameworkCore.Storage;
using UnitOfWork.Data;
using UnitOfWork.Models;
using UnitOfWork.Repositories.Implementations;
using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.Unit_Of_Work;

public class UnitOfWorks : IUnitOfWorks
{
    private readonly AppDbContext _context; // bu DbContext ni saqlash uchun
    private IDbContextTransaction? _transaction; // bu tranzaktsiyalarni boshqarish uchun
    public UnitOfWorks(AppDbContext context)
    {
        _context = context;
    }
    public virtual IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class // har qanday entity uchun repository olish uchun
    {
        return new GenericRepository<TEntity>(_context); // yangi GenericRepository yaratish va unga _context ni uzatish
    }
    public async Task<int> CommitAsync()
    {
        var result =  await _context.SaveChangesAsync(); // o'zgarishlarni saqlash uchun

        if(_transaction != null) // agar tranzaktsiya mavjud bo'lsa
        {
            await _transaction.CommitAsync(); // tranzaktsiyani yakunlash
            await _transaction.DisposeAsync(); // tranzaktsiyani ozod qilish
            _transaction = null; // tranzaktsiyani null qilish
        }
        return result;
    }

    public void Dispose()
    {
        _context.Dispose(); // bu resurslarni ozod qilish uchun
        _transaction?.Dispose(); // agar tranzaktsiya mavjud bo'lsa, uni ozod qilish
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null) // agar tranzaktsiya mavjud bo'lmasa
        {
            _transaction = await _context.Database.BeginTransactionAsync(); // yangi tranzaktsiya boshlash
        }

    }

    public async Task RollbackAsync()
    {
        if(_transaction != null) // agar tranzaktsiya mavjud bo'lsa
        {
            await _transaction.RollbackAsync(); // tranzaktsiyani bekor qilish
            await _transaction.DisposeAsync(); // tranzaktsiyani ozod qilish
            _transaction = null; // tranzaktsiyani null qilish
        }
    }
}
