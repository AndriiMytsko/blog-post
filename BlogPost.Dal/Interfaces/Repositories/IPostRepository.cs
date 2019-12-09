using BlogPost.Dal.Entities;
using System.Threading.Tasks;

namespace BlogPost.Dal.Interfaces.Repositories
{
    public interface IPostRepository : IRepository<PostEntity>
    {
        Task<PostEntity> GetPostWithCommentsAsync(int id);
    }
}