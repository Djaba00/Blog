using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;

namespace Blog.BLL.Services
{
    public class TagService : ITagService
    {
        IUnitOfWork db;
        IMapper mapper;

        public TagService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CreateTagAsync(TagModel tagModel)
        {
            var tag = mapper.Map<Tag>(tagModel);

            await db.Tags.CreateAsync(tag);

            await db.SaveAsync();
        }

        public async Task UpdateTagAsync(TagModel tagModel)
        {
            if (tagModel != null)
            {
                var tag = mapper.Map<Tag>(tagModel);

                await db.Tags.UpdateAsync(tag);

                await db.SaveAsync();
            }
        }

        public async Task DeleteTagAsync(int id)
        {
            await db.Tags.DeleteAsync(id);

            await db.SaveAsync();
        }

        public async Task<List<TagModel>> GetAllTagsAsync()
        {
            var tags = await db.Tags.GetAllAsync();

            var result = new List<TagModel>();

            foreach (var tag in tags)
            {
                result.Add(mapper.Map<TagModel>(tag));
            }

            return result;
        }

        public async Task<TagModel> GetTagByIdAsync(int id)
        {
            var tag = await db.Tags.GetByIdAsync(id);

            if (tag != null)
            {
                var result = mapper.Map<TagModel>(tag);

                return result;
            }
            else
            {
                throw new Exception("Тэг не найден");
            }
        }

        public async Task<TagModel> GetTagByName(string name)
        {
            var tag = await db.Tags.GetTagsByName(name);

            if (tag != null)
            {
                var result = mapper.Map<TagModel>(tag);

                return result;
            }
            else
            {
                throw new Exception("Тэг не найден");
            }
        }
    }
}
