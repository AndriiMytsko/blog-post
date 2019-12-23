using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces;
using BlogPost.Dal.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Dal.Ef.Repositories
{
    public class ImageRepository : Repository<ImageEntity>, IImageRepository
    {
        public ImageRepository(
          IUnitOfWork unitOfWork,
          BlogPostContext dbContext)
          : base(unitOfWork, dbContext)
        { }
    }
}
