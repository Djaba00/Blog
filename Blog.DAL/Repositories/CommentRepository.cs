using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task CreateAsync(Comment entity)
        {
            await db.Comments.AddAsync(entity);
        }

        public void Update(Comment entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var comment = db.Comments.Find(id);

            if (comment != null)
            {
                db.Entry(comment).State = EntityState.Deleted;
            }
        } 
    }
}
