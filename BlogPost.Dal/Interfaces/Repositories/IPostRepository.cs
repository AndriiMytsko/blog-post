using BlogPost.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Dal.Interfaces.Repositories
{
    public interface IPostRepository : IRepository<PostEntity>
    {
        Task<PostEntity> GetPostWithCommentsAsync(int id);
        Task<IList<PostEntity>> GetPostsWithUsersAsync();
    }
}