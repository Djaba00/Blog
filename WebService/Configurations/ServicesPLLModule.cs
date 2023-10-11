using Blog.BLL.Interfaces;
using Blog.BLL.Services;
using Blog.DAL.Interfaces;
using Blog.DAL.UnitOfWork;

namespace Blog.WebService.Configurations
{
    public static class ServicesPLLModule
    {
        public static IServiceCollection AddPllServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}
