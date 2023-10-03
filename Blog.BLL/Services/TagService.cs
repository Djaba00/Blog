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
    public class TagService : ITagService
    {
        IUnitOfWork db;
        IMapper mapper;

        public TagService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CreateCommentAsync(TagModel tagModel)
        {
            var tag = mapper.Map<Tag>(tagModel);

            await db.Tags.CreateAsync(tag);
        }

        public async Task UpdateCommentAsync(TagModel tagModel)
        {
            var tag = mapper.Map<Tag>(tagModel);

            db.Tags.Update(tag);
        }

        public async Task DeleteCommentAsync(int id)
        {
            db.Tags.Delete(id);
        }

        public async Task<List<TagModel>> GetAllComments()
        {
            var tags = await db.Tags.GetAllAsync();

            var result = new List<TagModel>();

            foreach (var tag in tags)
            {
                result.Add(mapper.Map<TagModel>(tag));
            }

            return result;
        }

        public async Task<TagModel> GetCommentById(int id)
        {
            var tag = await db.Tags.GetByIdAsync(id);

            var result = mapper.Map<TagModel>(tag);

            return result;
        }
    }
}
