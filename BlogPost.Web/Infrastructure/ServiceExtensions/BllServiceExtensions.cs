using BlogPost.Bll.Managers;
using BlogPost.Bll.Managers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPost.Web.Infrastructure.ServiceExtensions
{
    public static class BllServiceExtensions
    {
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddTransient<IBlogManager, BlogManager>();
            services.AddTransient<IPostManager , PostManager>();
            services.AddTransient<ICommentManager, CommentManager>();

            return services;
        }
    }
}