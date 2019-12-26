using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces;
using BlogPost.Dal.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Dal.Ef.Repositories
{
    public class CommentRepository : Repository<CommentEntity>, ICommnentRepository
    {
        public CommentRepository(
          IUnitOfWork unitOfWork,
          BlogPostContext dbContext)
          : base(unitOfWork, dbContext)
        { }

        public async Task<IList<CommentEntity>> CommentsAsync(int postId)
        {
            var comments = await DbContext.Comments
                .Where(comment => comment.Post.Id == postId)
                .ToListAsync();

            return comments;
        }
    }
}
