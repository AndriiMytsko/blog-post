using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces;
using BlogPost.Dal.Interfaces.Repositories;

namespace BlogPost.Dal.Ef.Repositories
{
    public class BlogRepository : Repository<BlogEntity>, IBlogRepository
    {
        public BlogRepository(
          IUnitOfWork unitOfWork,
          BlogPostContext dbContext)
          : base(unitOfWork, dbContext)
        { }
    }
}