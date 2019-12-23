using BlogPost.Dal.Entities;
using BlogPost.Dal.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPost.Dal.Ef
{
    public class BlogPostContext : 
        IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public BlogPostContext(DbContextOptions<BlogPostContext> options)
          : base(options)
        {
        }

        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<ImageEntity> Images{ get; set; }
    }
}