using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces;
using BlogPost.Dal.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Dal.Ef.Repositories
{
    public class PostRepository : Repository<PostEntity>, IPostRepository
    {
        public PostRepository(
          IUnitOfWork unitOfWork,
          BlogPostContext dbContext)
          : base(unitOfWork, dbContext)
        { }

        public async Task<IList<PostEntity>> GetLastPostsAsync()
        {
            var posts = await DbContext.Posts
                .OrderByDescending(p => p.CreatedAt)
                .Take(10)
                .ToListAsync();

            return posts;
        }

        public async Task<IList<PostEntity>> PostsAsync(int blogId)
        {
            var posts = await DbContext.Posts
                .Where(post => post.Blog.Id == blogId)
                .ToListAsync();

            return posts;
        }
    }
}