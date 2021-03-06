﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BlogPost.Bll.DTOs;

namespace BlogPost.Bll.Managers.Interfaces
{
    public interface IPostManager
    {
        Task<PostDto> GetPost(int id);
        Task<IList<PostDto>> GetAllPosts(int blogId);
        Task<IList<PostDto>> GetLastPosts();
        Task CreatePost(PostDto dto);
        Task UpdatePost(PostDto dto);
        Task DeletePost(int id);
    }
}

