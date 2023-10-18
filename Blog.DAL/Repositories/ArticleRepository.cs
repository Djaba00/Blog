using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Article>> GetArticlesByTitleAsync(string name)
        {
            var articles = await db.Articles
                .Include(a => a.Tags)
                .Include(a => a.Comments)
                .Where(a => a.Title.Contains(name))
                .ToListAsync();

            return articles;
        }

        public async Task<List<Article>> GetArticlesByAuthorIdAsync(int id)
        {
            var articles = await db.Articles
                .Include(a => a.Tags)
                .Include(a => a.Comments)
                .Where(a => a.UserId == id)
                .ToListAsync();

            return articles;
        }

        public async Task<List<Article>> GetArticlesByTagAsync(string tag)
        {
            var articles = await db.Articles
                .Include(a => a.Tags)
                .Include(a => a.Comments)
                .Where(a => a.Tags.Select(x => x.Name).Contains(tag))
                .ToListAsync();

            return articles;
        }

        public void Create(Article entity)
        {   
            db.Articles.Add(entity);
        }

        public void Update(Article updateEntity)
        {
            db.Articles.Update(updateEntity);
        }

        public void Delete(Article entity)
        {
            db.Articles.Remove(entity);
        }
    }
}
