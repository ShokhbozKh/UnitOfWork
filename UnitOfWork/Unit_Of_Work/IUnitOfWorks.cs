using UnitOfWork.Repositories.Interfaces;

namespace UnitOfWork.Unit_Of_Work
{
    public interface IUnitOfWorks: IDisposable // IDisposable - bu interface resurslarni tozalash uchun ishlatiladi
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class; // har qanday entity uchun repository olish uchun

        Task BeginTransactionAsync(); // tranzaktsiyani boshlash uchun
        Task RollbackAsync(); // tranzaktsiyani bekor qilish uchun
        Task<int> CommitAsync(); // o'zgarishlarni saqlash uchun
    }
}
