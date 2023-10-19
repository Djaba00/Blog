using AutoMapper;
using Blog.BLL.Externtions;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;

namespace Blog.BLL.Services
{
    public class ArticleService : IArticleService
    {
        IUnitOfWork db;
        IMapper mapper;

        public ArticleService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
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

        public async Task<List<ArticleModel>> GetArticlesByAuthorIdAsync(int authorId)
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

        public async Task UpdateArticleAsync(ArticleModel articleModel)
        {
            if (articleModel != null)
            {
                var article = await db.Articles.GetByIdAsync(articleModel.Id);

                var updateArticle = mapper.Map<Article>(articleModel);

                await article.Edit(updateArticle);

                await db.SaveAsync();
            }
        }

        public async Task DeleteArticleAsync(int id)
        {
            var article = await db.Articles.GetByIdAsync(id);
            
            db.Articles.Delete(article);

            await db.SaveAsync();
        }
    }
}
