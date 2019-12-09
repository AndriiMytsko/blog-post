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

        public async Task<PostEntity> GetPostWithCommentsAsync(int id)
        {
            var post = await DbContext.Posts
                    .Include(p => p.Comments)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(b => b.Id == id);

            return post;
        }
    }
}