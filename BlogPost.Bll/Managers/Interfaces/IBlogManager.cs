using BlogPost.Bll.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface IBlogManager
    {
        Task<BlogDto> GetBlog(int id);
        Task<IList<BlogDto>> GetAllBlogs();
        Task CreateBlog(BlogDto dto);
        Task UpdateBlog(BlogDto dto);
        Task DeleteBlog(int id);
    }
}
