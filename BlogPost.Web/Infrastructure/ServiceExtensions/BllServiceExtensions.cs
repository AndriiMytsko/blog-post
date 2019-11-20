using BlogPost.Bll.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPost.Web.Infrastructure.ServiceExtensions
{
    public static class BllServiceExtensions
    {
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddTransient<IBlogManager, BlogManager>();

            return services;
        }
    }
}