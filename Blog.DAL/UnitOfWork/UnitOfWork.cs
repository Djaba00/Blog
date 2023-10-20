using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Blog.DAL.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Blog.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        DataContext db;
        UserManager<UserAccount> userManager { get; set; }
        SignInManager<UserAccount> signInManager { get; set; }
        RoleManager<Role> roleManager { get; set; }

        IUserProfileRepository<UserProfile> UserProfileRepository { get; set; }
        IUserAccountRepository<UserAccount> UserAccountRepository { get; set; }
        IArticleRepository<Article> ArticleRepository { get; set; }
        ICommentRepository<Comment> CommentRepository { get; set; }
        ITagRepository<Tag> TagRepository { get; set; }

        private bool disposed = false;

        public UnitOfWork(DataContext db, UserManager<UserAccount> userManager, SignInManager<UserAccount> signInManager, RoleManager<Role> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public IUserProfileRepository<UserProfile> UserProfiles
        {
            get
            {
                if (UserProfileRepository == null)
                {
                    UserProfileRepository = new UserProfileRepository(db);
                }

                return UserProfileRepository;
            }
        }

        public IUserAccountRepository<UserAccount> UserAccounts
        {
            get
            {
                if (UserAccountRepository == null)
                {
                    UserAccountRepository = new UserAccountRepository(userManager);
                }

                return UserAccountRepository;
            }
        }

        public SignInManager<UserAccount> SignInManager
        {
            get
            {
                return signInManager;
            }
        }

        public RoleManager<Role> RoleManager
        {
            get
            {
                return roleManager;
            }
        }

        public IArticleRepository<Article> Articles
        {
            get
            {
                if (ArticleRepository == null)
                {
                    ArticleRepository = new ArticleRepository(db);
                }

                return ArticleRepository;
            }
        }

        public ICommentRepository<Comment> Comments
        {
            get
            {
                if (CommentRepository == null)
                {
                    CommentRepository = new CommentRepository(db);
                }

                return CommentRepository;
            }
        }

        public ITagRepository<Tag> Tags
        {
            get
            {
                if (TagRepository == null)
                {
                    TagRepository = new TagRepository(db);
                }

                return TagRepository;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
