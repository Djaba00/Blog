using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Article;
using Blog.WebService.ViewModels.Comment;
using Blog.WebService.ViewModels.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Blog
{
    [Route("Comment")]
    public class CommentController : Controller
    {
        IMapper mapper;
        ICommentService commentService;

        public CommentController(IMapper mapper, ICommentService commentService)
        {
            this.mapper = mapper;
            this.commentService = commentService;
        }

        [Authorize]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(ArticleViewModel articleModel)
        {
            var comment = mapper.Map<CommentModel>(articleModel.AddComment);

            await commentService.CreateCommentAsync(comment);

            return RedirectToAction("MyPage");
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateCommentAsync(EditCommentViewModel updateComment)
        {
            var comment = mapper.Map<CommentModel>(updateComment);

            comment.Changed = DateTime.Now;

            await commentService.UpdateCommentAsync(comment);

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

            return View("CommentList", models);
        }

        [HttpGet]
        [Route("ArticleComments/{id:int}")]
        public async Task<IActionResult> GetComentsByArticleIdAsync(int id)
        {
            var comments = await commentService.GetCommentsByArticleIdAsync(id);

            var models = new List<CommentModel>();

            foreach (var comment in comments)
            {
                models.Add(mapper.Map<CommentModel>(comment));
            }

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

            return View("CommentList", models);
        }
    }
}
