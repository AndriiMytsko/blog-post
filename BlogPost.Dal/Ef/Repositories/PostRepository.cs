using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces;
using BlogPost.Dal.Interfaces.Repositories;

namespace BlogPost.Dal.Ef.Repositories
{
    public class PostRepository : Repository<PostEntity>, IPostRepository
    {
        public PostRepository(
          IUnitOfWork unitOfWork,
          BlogPostContext dbContext)
          : base(unitOfWork, dbContext)
        { }
    }
}