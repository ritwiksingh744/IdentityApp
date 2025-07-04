using System.Linq.Expressions;

namespace IdentityApp.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<object> DeleteAsync(object id);
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task<IQueryable<T>> GetWithWhere(Expression<Func<T, bool>> query);
        Task<object> InsertAsync(T obj);
        Task<object> SaveAsync();
        Task<object> UpdateAsyc(T obj);
    }
}