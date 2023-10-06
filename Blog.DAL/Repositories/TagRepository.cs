using Blog.DAL.ApplicationContext;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.DAL.Repositories
{
    public class TagRepository : ITagRepository<Tag>
    {
        DataContext db;
        public TagRepository(DataContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await db.Tags
                .Include(t => t.Articles)
                .ToListAsync();

            return tags;
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            var tag = await db.Tags
                .Include(t => t.Articles)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tag != null)
            {
                return tag;
            }
            else
            {
                throw new Exception("Тэг не найден");
            }
        }

        public async Task CreateAsync(Tag entity)
        {
            await db.Tags.AddAsync(entity);
        }

        public async Task UpdateAsync(Tag entity)
        {
            var tag = await GetByIdAsync(entity.Id);

            if (tag != null)
            {
                tag.Name = entity.Name;
                tag.Articles = entity.Articles;
            }
            else
            {
                throw new Exception("Тэг не найден");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await GetByIdAsync(id);

            if (tag != null)
            {
                db.Tags.Remove(tag);
            }
        }

        public async Task<IEnumerable<Tag>> GetTagsByName(string name)
        {
            var tags = await db.Tags
                .Include(t => t.Articles)
                .Where(t => t.Name.Contains(name))
                .ToListAsync();

            return tags;
        }
    }
}
