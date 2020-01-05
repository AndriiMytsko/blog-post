using BlogPost.Bll.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface ICommentManager
    {
        Task CreateComment(CommentDto dto);
        Task<CommentDto> GetComment(int id);
        Task<IList<CommentDto>> GetComments(int postId);
        Task UpdateComment(CommentDto dto);
        Task DeleteComment(int id);
    }
}
