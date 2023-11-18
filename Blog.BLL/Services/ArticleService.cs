using System.Security.Claims;
using AutoMapper;
using Blog.BLL.Externtions;
using Blog.BLL.Exceptions;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace Blog.BLL.Services
{
    public class ArticleService : IArticleService
    {
        readonly ILogger<ArticleService> logger;
        readonly IUnitOfWork db;
        readonly IMapper mapper;
        readonly IAccountService accountService;

        public ArticleService(ILogger<ArticleService> logger, IUnitOfWork db, IMapper mapper, IAccountService accountService)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task<List<ArticleModel>> GetAllArticlesAsync()
        {
            var articles = await db.Articles.GetAllAsync();

            var result = new List<ArticleModel>();

            foreach (var article in articles)
            {
                result.Add(mapper.Map<ArticleModel>(article));
            }

            return result;
        }

        public async Task<ArticleModel> GetArticleByIdAsync(int id)
        {
            var article = await db.Articles.GetByIdAsync(id);

            var result = mapper.Map<ArticleModel>(article);

            return result;
        }

        public async Task<ArticleModel> GetArticleByIdForEditAsync(int id)
        {
            var article = await db.Articles.GetByIdAsync(id);

            var result = mapper.Map<ArticleModel>(article);

            var allTags = await db.Tags.GetAllAsync();

            result.Tags = new List<TagModel>();

            foreach (var tag in allTags)
            {
                var tempTag = mapper.Map<TagModel>(tag);

                result.Tags.Add(tempTag);
            }

            foreach (var tag in article.Tags.Select(t => t.Name))
            {
                result.Tags.FirstOrDefault(r => r.Name == tag).Selected = true;
            }

            return result;
        }

        public async Task<List<ArticleModel>> GetArticlesByAuthorIdAsync(string authorId)
        {
            var articles = await db.Articles.GetArticlesByAuthorIdAsync(authorId);

            var result = new List<ArticleModel>();

            foreach (var article in articles)
            {
                result.Add(mapper.Map<ArticleModel>(article));
            }

            return result;
        }

        public async Task<List<ArticleModel>> GetArticlesByTitleAsync(string title)
        {
            var articles = await db.Articles.GetArticlesByTitleAsync(title);

            var result = new List<ArticleModel>();

            foreach (var article in articles)
            {
                result.Add(mapper.Map<ArticleModel>(article));
            }

            return result;
        }

        public async Task<List<ArticleModel>> GetArticlesByTagAsync(string tagName)
        {
            var articles = await db.Articles.GetArticlesByTagAsync(tagName);

            var result = new List<ArticleModel>();

            foreach (var article in articles)
            {
                result.Add(mapper.Map<ArticleModel>(article));
            }

            return result;
        }

        public async Task<bool> CanChangeArticleAsync(ClaimsPrincipal claims, ArticleModel article)
        {
            var currentAccount = await accountService.GetAuthAccountAsync(claims);

            if (currentAccount.Id == article.UserId || currentAccount.IsInAnyRole("Admin", "Moderator"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ArticleModel> GetUpdateArticle(ClaimsPrincipal claims, int id)
        {
            try
            {
                var updateArticle = await GetArticleByIdAsync(id);

                var hasPermissions = await CanChangeArticleAsync(claims, updateArticle);

                if(!hasPermissions)
                {
                    throw new ForbiddenException();
                }

                var article = await db.Articles.GetByIdAsync(id);

                var allTags = await db.Tags.GetAllAsync();

                updateArticle.Tags = new List<TagModel>();

                foreach (var tag in allTags)
                {
                    var tempTag = mapper.Map<TagModel>(tag);

                    updateArticle.Tags.Add(tempTag);
                }

                foreach (var tag in article.Tags.Select(t => t.Name))
                {
                    updateArticle.Tags.FirstOrDefault(r => r.Name == tag).Selected = true;
                }

                return updateArticle;
            }
            catch (ForbiddenException ex)
            {
                logger.LogInformation("ERROR User-{0} doesn't have permissions to update article-{1}",
                    claims.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    id);

                throw new ForbiddenException();
            }
        }

        public async Task CreateArticleAsync(ArticleModel articleModel)
        {
            var article = mapper.Map<Article>(articleModel);

            article.Create();

            article.Tags = new List<Tag>();

            foreach (var tag in articleModel.Tags)
            {
                article.Tags.Add(await db.Tags.GetByIdAsync(tag.Id));
            }

            db.Articles.Create(article);

            await db.SaveAsync();
        }

        public async Task UpdateArticleAsync(ClaimsPrincipal claims, ArticleModel articleModel)
        {
            try
            {
                var hasPermissions = await CanChangeArticleAsync(claims, articleModel);

                if(!hasPermissions)
                {
                    throw new ForbiddenException();
                }

                articleModel.Tags = articleModel.GetSelectedTags();

                var article = await db.Articles.GetByIdAsync(articleModel.Id);

                var updateArticle = mapper.Map<Article>(articleModel);

                await article.Edit(updateArticle);

                await db.SaveAsync();
            }
            catch (ForbiddenException ex)
            {
                logger.LogInformation("ERROR User-{0} doesn't have permissions to update article-{1}",
                    claims.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    articleModel.Id);

                throw new ForbiddenException();
            }
        }

        public async Task DeleteArticleAsync(ClaimsPrincipal claims, int id)
        {
            try
            {
                var articleModel = await GetArticleByIdAsync(id);

                var hasPermissions = await CanChangeArticleAsync(claims, articleModel);

                if (!hasPermissions)
                {
                    throw new ForbiddenException();
                }

                var article = mapper.Map<Article>(articleModel);

                db.Articles.Delete(article);

                await db.SaveAsync();
            }
            catch (ForbiddenException ex)
            {
                logger.LogInformation("ERROR User-{0} doesn't have permissions to update article-{1}",
                    claims.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    id);

                throw new ForbiddenException();
            }
        }
    }
}
