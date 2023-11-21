using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Repositories
{
    public class CommentRepository : ICommentRepository<Comment>
    {
        DataContext db;
        public CommentRepository(DataContext context)
        {
            db = context;
        }

        public async Task<Comment> FindAsync(int id)
        {
            var result = await db.Comments.FindAsync(id);

            return result;
        }
        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            var comments = await db.Comments
                .Include(c => c.User)
                .ToListAsync();

            return comments;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await db.Comments.
                Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            return comment;
        }

        public async Task<List<Comment>> GetCommentsByAuthorIdAsync(string id)
        {
            var comments = await db.Comments
                .Include(c => c.User)
                .Where(c => c.UserId == id)
                .ToListAsync();

            return comments;
        }

        public async Task<List<Comment>> GetCommentsByArticleIdAsync(int id)
        {
            var comments = await db.Comments
                .Include(c => c.User)
                .Where(c => c.ArticleId == id)
                .ToListAsync();

            return comments;
        }

        public void Create(Comment entity)
        {
            db.Comments.Add(entity);
        }

        public void Update(Comment updateEntity)
        {
            db.Comments.Update(updateEntity);
        }

        public void Delete(Comment entity)
        {
            db.Comments.Remove(entity);
        }
    }
}
