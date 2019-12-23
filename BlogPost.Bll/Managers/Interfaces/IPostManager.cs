using System.Collections.Generic;
using System.Threading.Tasks;
using BlogPost.Bll.DTOs;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface IPostManager
    {
        Task<PostDto> GetPost(int id);
        Task<IList<PostDto>> GetAllPosts();
        Task CreatePost(PostDto dto);
        Task UpdatePost(PostDto dto);
        Task DeletePost(int id);
        Task<PostDto> GetPostWithComments(int id);
    }
}

