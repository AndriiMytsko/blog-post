using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Dal.Ef.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
      where TEntity : Entity
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly DbSet<TEntity> DbSet;
        protected BlogPostContext DbContext { get; }

        protected Repository(
          IUnitOfWork unitOfWork,
          BlogPostContext dbContext)
        {
            UnitOfWork = unitOfWork;
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        protected IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>();
        }

        public async Task<IList<TEntity>> GetAsync()
        {
            return await GetAll().ToArrayAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await DbContext.FindAsync<TEntity>(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Added;

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Deleted;

            await DbContext.SaveChangesAsync();
        }
    }
}