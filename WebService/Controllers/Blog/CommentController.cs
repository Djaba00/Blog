using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.VIewModels.Comment;
using Blog.WebService.VIewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Blog
{
    public class CommentController : Controller
    {
        IMapper mapper;
        ICommentService commentService;
        IAccountService accountService;

        public CommentController(IMapper mapper, ICommentService commentService, IAccountService accountService)
        {
            this.mapper = mapper;
            this.commentService = commentService;
            this.accountService = accountService;
        }

        [Authorize]
        [Route("NewComment")]
        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(CreateCommentViewModel commentModel)
        {
            var comment = mapper.Map<CommentModel>(commentModel);

            comment.Created = DateTime.Now;

            await commentService.CreateCommentAsync(comment);

            return RedirectToAction("MyPage");
        }

        [Authorize]
        [Route("EditComment")]
        [HttpPost]
        public async Task<IActionResult> UpdateCommentAsync(EditCommentViewModel updateComment)
        {
            var comment = mapper.Map<CommentModel>(updateComment);

            comment.Changed = DateTime.Now;

            await commentService.UpdateCommentAsync(comment);

            return RedirectToAction("MyPage");
        }

        [Authorize]
        [Route("DeleteComment")]
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

            return View("CommentsList", models);
        }

        [HttpGet]
        [Route("Comment/ArticleComments")]
        public async Task<IActionResult> GetComentsByArticleIdAsync(int id)
        {
            var comments = await commentService.GetCommentsByArticleIdAsync(id);

            var models = new List<CommentModel>();

            foreach (var comment in comments)
            {
                models.Add(mapper.Map<CommentModel>(comment));
            }

            return View("CommentsList", models);
        }

        [HttpGet]
        [Route("Comment/AuthorComments")]
        public async Task<IActionResult> GetCommentsByAuthorIdAsync(string id)
        {
            var comments = await commentService.GetCommentsByAuthorIdAsync(id);

            var models = new List<CommentModel>();

            foreach (var comment in comments)
            {
                models.Add(mapper.Map<CommentModel>(comment));
            }

            return View("CommentsList", models);
        }
    }
}
