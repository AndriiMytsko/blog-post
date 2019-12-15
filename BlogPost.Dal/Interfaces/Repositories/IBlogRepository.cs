using BlogPost.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Dal.Interfaces.Repositories
{
    public interface IBlogRepository : IRepository<BlogEntity>
    {
        Task<BlogEntity> GetBlogWithPostsAsync(int id);
        Task<IList<BlogEntity>> GetBlogsWithUsersAsync();
    }
}