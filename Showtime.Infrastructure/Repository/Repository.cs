using Microsoft.EntityFrameworkCore;
using Showtime.Core.Entities;
using Showtime.Core.Interfaces.Repository;
using Showtime.Infrastructure.Database;
using System.Linq.Expressions;

namespace Showtime.Infrastructure.Repository
{
    public class Repository<TEntity>(DataContext context) : IRepository<TEntity> where TEntity : BaseUser
    {
        private readonly DataContext _context = context;
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<TEntity?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public IEnumerable<TEntity> Find(
    Expression<Func<TEntity, bool>>[]? filters = null,
    Expression<Func<TEntity, object>>[]? includes = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return [.. query];
        }

        public async Task InsertAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public async Task UpdateAsync(TEntity entity)
        {
            TEntity? entity1 = await GetByIdAsync(entity.Id);

            if (entity1 != null)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            TEntity? entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Delete(TEntity entity) => _dbSet.Remove(entity);
    }
}
