using BCS.Core.Entities;

namespace BCS.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllByUserAsync(AppUser user);
        Task<TEntity> GetAsync(TKey id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
        Task SaveAsync();
    }
}
