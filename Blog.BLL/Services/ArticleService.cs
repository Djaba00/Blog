using AutoMapper;
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

        public async Task CreateArticleAsync(ArticleModel articleModel)
        {
            var article = mapper.Map<Article>(articleModel);

            await db.Articles.CreateAsync(article);

            await db.SaveAsync();
        }

        public async Task UpdateArticleAsync(ArticleModel articleModel)
        {
            if (articleModel != null)
            {
                var article = mapper.Map<Article>(articleModel);

                await db.Articles.UpdateAsync(article);

                await db.SaveAsync();
            }
        }

        public async Task DeleteArticleAsync(int id)
        {
            await db.Articles.DeleteAsync(id);

            await db.SaveAsync();
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

        public async Task<List<ArticleModel>> GetArticlesByAuthotIdAsync(string authorId)
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

        public async Task<List<ArticleModel>> GetArticlesByTagAsync(string tag)
        {
            var articles = await db.Articles.GetArticlesByTagAsync(tag);

            var result = new List<ArticleModel>();

            foreach (var article in articles)
            {
                result.Add(mapper.Map<ArticleModel>(article));
            }

            return result;
        }
    }
}
