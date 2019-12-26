using System.Collections.Generic;
using System.Threading.Tasks;
using BlogPost.Bll.DTOs;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface IBlogManager
    {
        Task CreateBlog(BlogDto dto);
        Task<BlogDto> GetBlog(int id);
        Task<IList<BlogDto>> GetAllBlogs();
        Task UpdateBlog(BlogDto dto);
        Task DeleteBlog(int id);
    }
}