using Blog.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserProfileRepository<UserProfile> UserProfiles { get; }
        IUserAccountRepository<UserAccount> UserAccounts { get; }
        SignInManager<UserAccount> AccountSignIn { get; }
        IArticleRepository<Article> Articles { get; }
        ICommentRepository<Comment> Comments { get; }
        ITagRepository<Tag> Tags { get; }

        Task SaveAsync();
    }
}
