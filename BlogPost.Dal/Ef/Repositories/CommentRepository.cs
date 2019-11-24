using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces;
using BlogPost.Dal.Interfaces.Repositories;

namespace BlogPost.Dal.Ef.Repositories
{
    public class CommentRepository : Repository<CommentEntity>, ICommnentRepository
    {
        public CommentRepository(
          IUnitOfWork unitOfWork,
          BlogPostContext dbContext)
          : base(unitOfWork, dbContext)
        { }
    }
}
