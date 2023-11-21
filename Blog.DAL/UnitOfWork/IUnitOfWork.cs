using Blog.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserProfileRepository<UserProfile> UserProfiles { get; }
        IUserAccountRepository UserAccounts { get; }
        SignInManager<UserAccount> SignInManager { get; }
        RoleManager<Role> RoleManager { get; }
        IArticleRepository<Article> Articles { get; }
        ICommentRepository<Comment> Comments { get; }
        ITagRepository<Tag> Tags { get; }

        Task SaveAsync();
    }
}
