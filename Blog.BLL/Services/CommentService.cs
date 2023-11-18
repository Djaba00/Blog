using System.Security.Claims;
using AutoMapper;
using Blog.BLL.Exceptions;
using Blog.BLL.Externtions;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace Blog.BLL.Services
{
    public class CommentService : ICommentService
    {
        readonly ILogger<CommentService> logger;
        readonly IUnitOfWork db;
        readonly IMapper mapper;
        readonly IAccountService accountService;

        public CommentService(ILogger<CommentService> logger, IUnitOfWork db, IMapper mapper, IAccountService accountService)
        {
            this.logger = logger;
            this.db = db;
            this.mapper = mapper;
            this.accountService = accountService;
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

        public async Task<bool> CanChangeCommentAsync(ClaimsPrincipal claims, CommentModel commentModel)
        {
            var currentAccount = await accountService.GetAuthAccountAsync(claims);

            if (currentAccount.Id == commentModel.UserId || currentAccount.IsInAnyRole("Admin", "Moderator"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<CommentModel> GetUpdateCommentAsync(ClaimsPrincipal claims, int id)
        {
            try
            {
                var updateComment = await GetCommentByIdAsync(id);

                var hasPermissions = await CanChangeCommentAsync(claims, updateComment);

                if (!hasPermissions)
                {
                    throw new ForbiddenException();
                }

                return updateComment;
            }
            catch (ForbiddenException ex)
            {
                logger.LogInformation("ERROR User-{0} doesn't have permissions to update comment-{1}",
                    claims.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    id);

                throw new ForbiddenException();
            }
        }

        public async Task CreateCommentAsync(CommentModel commentModel)
        {
            if (commentModel != null)
            {
                var comment = mapper.Map<Comment>(commentModel);

                comment.Created = DateTime.Now;

                db.Comments.Create(comment);

                await db.SaveAsync();
            }
        }

        public async Task UpdateCommentAsync(ClaimsPrincipal claims, CommentModel commentModel)
        {
            try
            {
                var hasPermissions = await CanChangeCommentAsync(claims, commentModel);

                if (!hasPermissions)
                {
                    throw new ForbiddenException();
                }

                var comment = await db.Comments.GetByIdAsync(commentModel.Id);

                var updateComment = mapper.Map<Comment>(commentModel);

                comment.Edit(updateComment);

                await db.SaveAsync();
            }
            catch (ForbiddenException ex)
            {
                logger.LogInformation("ERROR User-{0} doesn't have permissions to update comment-{1}",
                    claims.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    commentModel.Id);

                throw new ForbiddenException();
            }
        }

        public async Task DeleteCommentAsync(ClaimsPrincipal claims, int id)
        {
            try
            {
                var commentModel = await GetCommentByIdAsync(id);

                var hasPermissions = await CanChangeCommentAsync(claims, commentModel);

                if (!hasPermissions)
                {
                    throw new ForbiddenException();
                }

                var comment = mapper.Map<Comment>(commentModel);

                db.Comments.Delete(comment);

                await db.SaveAsync();
            }
            catch (ForbiddenException ex)
            {
                logger.LogInformation("ERROR User-{0} doesn't have permissions to update comment-{1}",
                    claims.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    id);

                throw new ForbiddenException();
            }
        }
    }
}
