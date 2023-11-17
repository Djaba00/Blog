using System;
using Blog.BLL.Interfaces;
using Blog.BLL.Services;

namespace Blog.API.Configurations
{
	public static class ServicesAPIModule
	{
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IAccountRoleService, AccountRoleService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}

