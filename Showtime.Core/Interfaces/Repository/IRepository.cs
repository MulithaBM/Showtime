using Showtime.Core.Entities;
using System.Linq.Expressions;

namespace Showtime.Core.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>>[]? filters = null,
            Expression<Func<TEntity, object>>[]? includes = null);
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteByIdAsync(Guid id);
        void Delete(TEntity entity);
    }
}
