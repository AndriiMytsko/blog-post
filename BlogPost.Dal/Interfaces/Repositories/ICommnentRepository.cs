﻿using BlogPost.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Dal.Interfaces.Repositories
{
    public interface ICommnentRepository : IRepository<CommentEntity>
    {
        Task<IList<CommentEntity>> CommentsAsync(int postId);
    }
}
