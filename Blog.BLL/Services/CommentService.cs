using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;

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
            if (commentModel != null)
            {
                var comment = mapper.Map<Comment>(commentModel);

                await db.Comments.CreateAsync(comment);

                await db.SaveAsync();
            }
        }

        public async Task UpdateCommentAsync(CommentModel commentModel)
        {
            if (commentModel != null)
            {
                var comment = mapper.Map<Comment>(commentModel);

                await db.Comments.UpdateAsync(comment);

                await db.SaveAsync();
            }
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comment = db.Comments.DeleteAsync(id);

            await db.SaveAsync();
        }

        public async Task<List<CommentModel>> GetAllCommentsAsync()
        {
            var comments = await db.Comments.GetAllAsync();

            var result = new List<CommentModel>();

            foreach (var comment in comments)
            {
                result.Add(mapper.Map<CommentModel>(comment));
            }

            return result;
        }

        public async Task<CommentModel> GetCommentByIdAsync(int id)
        {
            var comment = await db.Comments.GetByIdAsync(id);

            var result = mapper.Map<CommentModel>(comment);

            return result;
        }

        public async Task<List<CommentModel>> GetCommentsByAuthorIdAsync(string id)
        {
            var comments = await db.Comments.GetCommentsByAuthorIdAsync(id);

            var result = new List<CommentModel>();

            foreach (var comment in comments)
            {
                result.Add(mapper.Map<CommentModel>(comment));
            }

            return result;
        }

        public async Task<List<CommentModel>> GetCommentsByArticleIdAsync(int id)
        {
            var comments = await db.Comments.GetCommentsByArticleIdAsync(id);

            var result = new List<CommentModel>();

            foreach (var comment in comments)
            {
                result.Add(mapper.Map<CommentModel>(comment));
            }

            return result;
        }
    }
}
