using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IdentityApp.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ProjDbContext _context;
        private DbSet<T> _entity;
        public GenericRepository(ProjDbContext cartDbContext)
        {
            _context = cartDbContext;
            _entity = _context.Set<T>();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return _entity;
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<object> InsertAsync(T obj)
        {
            await _entity.AddAsync(obj);
            return await SaveAsync();
        }

        public async Task<object> UpdateAsyc(T obj)
        {
            _entity.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return await SaveAsync();
        }

        public async Task<object> DeleteAsync(object id)
        {
            var obj = await _entity.FindAsync(id);
            if (obj != null)
            {
                _context.Remove(obj);
            }
            return await SaveAsync();
        }

        public async Task<IQueryable<T>> GetWithWhere(Expression<Func<T, bool>> query)
        {
            return _entity.Where(query);
        }

        public async Task<object> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
