using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blog.DAL.Repositories
{
    public class ArticleRepository : IArticleRepository<Article>
    {
        DataContext db;
        public ArticleRepository(DataContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            var articles = await db.Articles
                .Include(a => a.Tags)
                .Include(a => a.Comments)
                .ToListAsync();

            return articles;
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            var article = await db.Articles
                .Include(a => a.Tags)
                .Include(a => a.Comments)
                .FirstOrDefaultAsync(a => a.Id == id);

            return article;
        }

        public async Task CreateAsync(Article entity)
        {
            await db.Articles.AddAsync(entity);
        }

        public void Update(Article entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var article = db.Articles.Find(id);

            if (article != null)
            {
                db.Entry(article).State = EntityState.Deleted;
            }
        }

        public async Task<List<Article>> GetArticlesByName(string name)
        {
            var articles = await db.Articles
                .Include(a => a.Tags)
                .Include(a => a.Comments)
                .Where(a => a.Name.Contains(name))
                .ToListAsync();

            return articles;
        }

        public async Task<List<Article>> GetArticlesByAuthorId(string id)
        {
            var articles = await db.Articles
                .Include(a => a.Tags)
                .Include(a => a.Comments)
                .Where(a => a.UserId == id)
                .ToListAsync();

            return articles;
        }
    }
}
