using BlogPost.Dal.Ef;
using BlogPost.Dal.Ef.Repositories;
using BlogPost.Dal.Interfaces;
using BlogPost.Dal.Interfaces.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPost.Web.Infrastructure.ServiceExtensions
{
    public static class EfDalServiceExtensions
    {
        public static IServiceCollection AddEfDal(
          this IServiceCollection services,
          IConfiguration configuration)
        {
            services.AddDbContext<BlogPostContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("BlogPostDbConnectionString")));

            services.AddTransient<IBlogRepository, BlogRepository>();
            services.AddTransient<IDbTransaction, DbTransaction>();
            services.AddTransient<IUnitOfWork, UnitOfWork<BlogPostContext>>();

            return services;
        }

        public static void CreateEfDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BlogPostContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}