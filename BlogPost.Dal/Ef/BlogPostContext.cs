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
        public DbSet<ImageEntity> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostEntity>()
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentEntity>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
    .HasMany(e => e.Comments)
    .WithOne(e => e.User)
    .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<ApplicationUser>()
               .HasMany(e => e.Posts)
               .WithOne(e => e.User)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}