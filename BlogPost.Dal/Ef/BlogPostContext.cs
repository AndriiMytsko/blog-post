using BlogPost.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Dal.Ef
{
    public class BlogPostContext : DbContext
    {
        public BlogPostContext(DbContextOptions<BlogPostContext> options)
          : base(options)
        {
        }

        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
    }
}