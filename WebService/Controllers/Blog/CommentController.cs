using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
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
            logger.LogInformation("{0} POST User-{1} send newComment data",
                DateTime.UtcNow.ToLongTimeString(),
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var comment = mapper.Map<CommentModel>(articleModel.AddComment);

            await commentService.CreateCommentAsync(comment);

            logger.LogInformation("{0} POST User-{1} created comment",
                DateTime.UtcNow.ToLongTimeString(),
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction("MyPage");
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateCommentAsync(EditCommentViewModel updateComment)
        {
            logger.LogInformation("{0} POST User-{1} send updateComment data",
                DateTime.UtcNow.ToLongTimeString(),
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var comment = mapper.Map<CommentModel>(updateComment);

            comment.Changed = DateTime.Now;

            await commentService.UpdateCommentAsync(comment);

            logger.LogInformation("{0} POST User-{1} updated comment",
                DateTime.UtcNow.ToLongTimeString(),
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction("MyPage");
        }

        [Authorize]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            await commentService.DeleteCommentAsync(id);

            return RedirectToAction("MyPage");
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

            logger.LogInformation("{0} GET CommentList page responsed",
                DateTime.UtcNow.ToLongTimeString());

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

            logger.LogInformation("{0} GET CommentList by user-{1} page responsed",
                DateTime.UtcNow.ToLongTimeString(),
                id);

            return View("CommentList", models);
        }
    }
}
