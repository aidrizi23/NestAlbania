namespace NestAlbania.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task Create(TEntity entity);
        Task Edit(TEntity entity);
        Task Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
    }
}