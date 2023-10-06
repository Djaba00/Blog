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

        public async Task CreateAsync(Comment entity)
        {
            await db.Comments.AddAsync(entity);
        }

        public async Task UpdateAsync(Comment updateEntity)
        {
            var comment = await GetByIdAsync(updateEntity.Id);

            if (comment != null)
            {
                comment.Changed = DateTime.Now;
                comment.Content = updateEntity.Content;
            }
            else
            {
                throw new Exception("Комментарий не найден");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await GetByIdAsync(id);

            if (comment != null)
            {
                db.Comments.Remove(comment);
            }
        }
    }
}
