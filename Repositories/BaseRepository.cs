using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;

namespace NestAlbania.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task Create(TEntity entity)
        {
            await _applicationDbContext.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Edit(TEntity entity)
        {
            _applicationDbContext.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _applicationDbContext.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _applicationDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _applicationDbContext.Set<TEntity>().FindAsync(id);
        }
    }
}
