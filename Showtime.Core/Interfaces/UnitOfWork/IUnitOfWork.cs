using Showtime.Core.Entities;
using Showtime.Core.Interfaces.Repository;

namespace Showtime.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseUser;
        Task SaveAsync();
    }
}
