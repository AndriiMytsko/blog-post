using BlogPost.Bll.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface ICommentManager
    {
        Task<CommentDto> GetComment(int id);
        Task<IList<CommentDto>> GetAllComments();
        Task CreateComment(CommentDto dto);
        Task UpdateComment(CommentDto dto);
        Task DeleteComment(int id);
    }
}
