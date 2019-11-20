using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Dal.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<IList<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(int id);
    }
}