using AutoMapper;
using Blog.BLL.Exceptions;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.DAL.Entities;
using Blog.WebService.ViewModels.Article;
using Blog.WebService.ViewModels.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Blog
{
    [Route("Comment")]
    public class CommentController : Controller
    {
        readonly ILogger<CommentController> logger;
        readonly IMapper mapper;
        readonly ICommentService commentService;

        public CommentController(ILogger<CommentController> logger, IMapper mapper, ICommentService commentService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.commentService = commentService;
        }

        [Authorize]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(ArticleViewModel articleModel)
        {
            logger.LogInformation("POST User-{0} send newComment data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var comment = mapper.Map<CommentModel>(articleModel.AddComment);

            await commentService.CreateCommentAsync(comment);

            logger.LogInformation("POST User-{0} created comment",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction($"Article", "Article", new { id = comment.ArticleId });
        }

        [Authorize]
        [Route("Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> UpdateArticleAsync(int id)
        {
            try
            {
                var comment = await commentService.GetUpdateCommentAsync(User, id);

                var model = mapper.Map<EditCommentViewModel>(comment);

                logger.LogInformation("GET EditComment page responsed for user-{0}",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return View("EditComment", model);
            }
            catch (ForbiddenException)
            {
                return RedirectToAction("403", "Error");
            }
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateCommentAsync(EditCommentViewModel updateComment)
        {
            try
            {
                logger.LogInformation("POST User-{0} send updateComment data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var comment = mapper.Map<CommentModel>(updateComment);

                comment.Changed = DateTime.Now;

                await commentService.UpdateCommentAsync(User, comment);

                logger.LogInformation("POST User-{0} updated comment",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return RedirectToAction($"Article", "Article", new { id = comment.ArticleId });
            }
            catch (ForbiddenException)
            {
                return RedirectToAction("403", "Error");
            }
        }

        [Authorize]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteCommentAsync(int commentId, int articleId)
        {
            try
            {
                await commentService.DeleteCommentAsync(User, commentId);

                return RedirectToAction($"Article", "Article", new { id = articleId});
            }
            catch (ForbiddenException)
            {
                return RedirectToAction("403", "Error");
            }
        }

        [HttpGet]
        [Route("Comments")]
        public async Task<IActionResult> GetCommentsListAsync()
        {
            var comments = await commentService.GetAllCommentsAsync();

            var models = new List<CommentModel>();

            foreach (var comment in comments)
            {
                models.Add(mapper.Map<CommentModel>(comment));
            }

            logger.LogInformation("GET CommentList page responsed");

            return View("CommentList", models);
        }

        [HttpGet]
        [Route("AuthorComments/{id:int}")]
        public async Task<IActionResult> GetCommentsByAuthorIdAsync(string id)
        {
            var comments = await commentService.GetCommentsByAuthorIdAsync(id);

            var models = new List<CommentModel>();

            foreach (var comment in comments)
            {
                models.Add(mapper.Map<CommentModel>(comment));
            }

            logger.LogInformation("GET CommentList by user-{0} page responsed",
                id);

            return View("CommentList", models);
        }
    }
}
