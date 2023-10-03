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
using System.Xml.Linq;

namespace Blog.BLL.Services
{
    public class CommentService : ICommentService
    {
        IUnitOfWork db;
        IMapper mapper;

        public CommentService(IUnitOfWork db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CreateCommentAsync(CommentModel commentModel)
        {
            var comment = mapper.Map<Comment>(commentModel);

            await db.Comments.CreateAsync(comment);
        }

        public async Task UpdateCommentAsync(CommentModel commentModel)
        {
            var comment = mapper.Map<Comment>(commentModel);

            db.Comments.Update(comment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            db.Comments.Delete(id);
        }

        public async Task<List<CommentModel>> GetAllComments()
        {
            var comments = await db.Comments.GetAllAsync();

            var result = new List<CommentModel>();

            foreach (var comment in comments)
            {
                result.Add(mapper.Map<CommentModel>(comment));
            }

            return result;
        }

        public async Task<CommentModel> GetCommentById(int id)
        {
            var comment = await db.Comments.GetByIdAsync(id);

            var result = mapper.Map<CommentModel>(comment);

            return result;
        }
    }
}
