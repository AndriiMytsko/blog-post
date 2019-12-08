using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces;
using BlogPost.Dal.Interfaces.Repositories;
using System.Threading.Tasks;

namespace BlogPost.Dal.Ef.Repositories
{
    public class BlogRepository : Repository<BlogEntity>, IBlogRepository
    {
        public BlogRepository(
          IUnitOfWork unitOfWork,
          BlogPostContext dbContext)
          : base(unitOfWork, dbContext)
        { }

        public async Task<BlogEntity> GetBlogWithPostsAsync(int id)
        {
            var blog = await DbContext.FindAsync<BlogEntity>(id);

            DbContext.Entry(blog)
                    .Collection(b => b.Posts)
                    .Load();

            return blog;
        }
    }
}