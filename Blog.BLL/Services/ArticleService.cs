using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task CreateArticelAsync(ArticleModel articleModel)
        {
            var article = mapper.Map<Article>(articleModel);

            await db.Articles.CreateAsync(article);
        }

        public async Task UpdateArticleAsync(ArticleModel articleModel)
        {
            var article = mapper.Map<Article>(articleModel);

            db.Articles.Update(article);
        }

        public async Task DeleteArticelAsync(int id)
        {
            db.Articles.Delete(id);
        }

        public async Task<List<ArticleModel>> GetAllArticles()
        {
            var articles = await db.Articles.GetAllAsync();

            var result = new List<ArticleModel>();

            foreach (var article in articles)
            {
                result.Add(mapper.Map<ArticleModel>(article));
            }

            return result;
        }

        public async Task<List<ArticleModel>> GetArticlesByAuthotId(string id)
        {
            var articles = await db.Articles.GetArticlesByAuthorId(id);

            var result = new List<ArticleModel>();

            foreach (var article in articles)
            {
                result.Add(mapper.Map<ArticleModel>(article));
            }

            return result;
        }
    }
}
