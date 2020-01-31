using BlogPost.Bll.Managers;
using BlogPost.Bll.Managers.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlogPost.Web.Infrastructure.ServiceExtensions
{
    public static class BllServiceExtensions
    {
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddTransient<IBlogManager, BlogManager>();
            services.AddTransient<IPostManager, PostManager>();
            services.AddTransient<ICommentManager, CommentManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<Func<SmtpClient>>(serviceProvides => () => new SmtpClient());

            services.AddTransient<IEmailManager>(serviceProvides => new EmailManager(
               serviceProvides.GetRequiredService<Func<SmtpClient>>()));

            return services;
        }
    }
}