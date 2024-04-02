using Showtime.Core.Entities;
using Showtime.Core.Interfaces.Repository;
using Showtime.Core.Interfaces.UnitOfWork;
using Showtime.Infrastructure.Database;
using Showtime.Infrastructure.Repository;

namespace Showtime.Infrastructure.UnitOfWork
{
    public class UnitOfWork(DataContext context) : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context = context;
        private readonly Dictionary<Type, object> repositories = [];
        private bool disposed = false;

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseUser
        {
            if (!repositories.ContainsKey(typeof(TEntity)))
            {
                repositories[typeof(TEntity)] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>)repositories[typeof(TEntity)];
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
