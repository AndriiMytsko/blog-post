using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces;
using BlogPost.Dal.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
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
            var blogEntity = await DbContext.Blogs
                .Include(blog => blog.Posts)
                   .ThenInclude(p => p.User)
                .Include(p => p.User)
                .FirstOrDefaultAsync(b => b.Id == id);

            return blogEntity;
        }
    }
}